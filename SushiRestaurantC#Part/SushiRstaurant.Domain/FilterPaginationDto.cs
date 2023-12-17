using SushiRestaurant.Constants;

namespace SushiRstaurant.Domain;

public sealed record FilterPaginationDto(
    string SearchTerm = "",
    int PageNumber = 1,
    int PageSize = 50,
    string SortColumn = Constants.IdStringName,
    SortOrder SortOrder = SortOrder.Asc);