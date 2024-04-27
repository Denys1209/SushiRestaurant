using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SushiRestaurant.Application.Dishes;
using SushiRestaurant.Application.OrderDishes;
using SushiRestaurant.Application.Orders;
using SushiRestaurant.Application.Users;
using SushiRestaurant.WebApi.Dtos.OrderDishDtos;
using SushiRestaurant.WebApi.Dtos.OrderDtos;
using SushiRestaurant.WebApi.Dtos.UserDtos;
using SushiRestaurant.WebApi.Filters.Validation;
using SushiRstaurant.Domain;
using SushiRstaurant.Domain.Models;

namespace SushiRestaurant.WebApi.Controllers;
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IDishService _dishService;
    private readonly IOrderDishService _orderDishService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public OrderController(IDishService dishService, IOrderDishService orderDishService, IUserService userService, IOrderService orderService, IMapper mapper) 
    {
        _dishService = dishService;
        _orderDishService = orderDishService;
        _userService = userService;
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FilterPaginationDto paginationDto, CancellationToken cancellationToken)
    {
        var orders = _mapper.Map<List<GetOrderDto>>(await _orderService.GetAllAsync(paginationDto, cancellationToken));
        foreach (var order in orders)
        {
            order.OrderDishDtos = _mapper.Map<List<GetOrderDishDto>>(await _orderDishService.GetAllOrderDishesInOrderIdAsync(order.Id, cancellationToken));
            if (order.User != null)
            {
                order.User = _mapper.Map<GetUserDto>(await _userService.GetAsync(order.User.Id, cancellationToken))!;
            }
        }
        int howManyPages = await _orderService.GetNumberOfPagesAsync(paginationDto.PageSize, cancellationToken);

        return Ok(new ReturnOrderPageDto { HowManyPages = howManyPages, Orders=orders });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<GetOrderDto>(await _orderService.GetAsync(id, cancellationToken));
        if (order is null)
            return NotFound();
        order.OrderDishDtos = _mapper.Map<List<GetOrderDishDto>>(await _orderDishService.GetAllOrderDishesInOrderIdAsync(order.Id, cancellationToken));
        return Ok(_mapper.Map<GetOrderDto>(order));
    }

    [ValidationFilter]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("createOrder")]
    public async Task<IActionResult> Post([FromQuery] int? userId, [FromQuery] List<int> dishesId, [FromQuery] List<uint> dishesQuantity, [FromBody] CreateOrderDto dto, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(dto);
        if (userId != null)
        {
            var user = await _userService.GetAsync(userId ?? 0, cancellationToken);
            if (user is null)
            {
                ModelState.AddModelError("", $"User with {userId} id doesn't exist");
                return StatusCode(422, ModelState);
            }
            order.User = user;
        }
        else 
        {
            order.User = null;
        }
        var dishes = (await _dishService.GetAllModelsByIdsAsync(dishesId, cancellationToken)).ToArray();
        for (var i = 0; i < dishesId.Count(); ++i)
        {
            if (dishes[i] is null)
            {
                ModelState.AddModelError("", $"Dish with {dishesId[i]} doesn't exist");
                return StatusCode(422, ModelState);
            }
        }
        var id = await _orderService.CreateAsync(order, cancellationToken);
        var createdOrder = await _orderService.GetAsync(id, cancellationToken);
        if (createdOrder is null)
        {
            ModelState.AddModelError("", $"Order wasn't created");
            return StatusCode(422, ModelState);
        }

        for (int i = 0; i < dishes.Count(); ++i)
        {
            await _orderDishService.CreateAsync(new OrderDish { Dish = dishes[i]!, Order = order!, quantity = dishesQuantity[i] }, cancellationToken);
        }

        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put([FromQuery] int? userId, [FromQuery] List<int> dishesId, [FromQuery] List<uint> dishesQuantity, [FromBody] CreateOrderDto dto, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(dto);
        User? user = null;
        if (userId != null)
        {
            user = await _userService.GetAsync((int)userId, cancellationToken);
            if (user is null)
            {
                ModelState.AddModelError("", $"User with {userId} id doesn't exist");
                return StatusCode(422, ModelState);
            }
        }
        order.User = user;
        var dishes = (await _dishService.GetAllModelsByIdsAsync(dishesId, cancellationToken)).ToArray();
        for (var i = 0; i < dishesId.Count(); ++i)
        {
            if (dishes[i] is null)
            {
                ModelState.AddModelError("", $"Dish with {dishesId[i]} doesn't exist");
                return StatusCode(422, ModelState);
            }
        }
        await _orderService.UpdateAsync(order, cancellationToken);
        var updateOrder = await _orderService.GetAsync(order.Id, cancellationToken);
        if (updateOrder is null)
        {
            ModelState.AddModelError("", $"Order wasn't created");
            return StatusCode(422, ModelState);
        }
        foreach (var item in updateOrder.OrderDishes)
        {
            await _orderDishService.DeleteAsync(item.Id, cancellationToken);
        }
        for (int i = 0; i < dishes.Count(); ++i)
        {
            await _orderDishService.CreateAsync(new OrderDish { Dish = dishes[i]!, Order = order!, quantity = dishesQuantity[i] }, cancellationToken);
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var orderToDelete = await _orderService.GetAsync(id, cancellationToken);
        if (orderToDelete == null)
        {
            return NotFound($"Order with Id = {id} not found");
        }

        await _orderService.DeleteAsync(orderToDelete.Id, cancellationToken);

        return NoContent();
    }



}
