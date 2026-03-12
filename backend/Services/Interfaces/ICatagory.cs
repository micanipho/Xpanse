using server.Dtos;

namespace server.Services.Interfaces
{
    public interface ICategory
    {
        Task<List<CategoryResponse>> GetAllCategoriesAsync();
        Task<CategoryResponse?> GetCategoryByIdAsync(int id);
        Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest category);
        Task<bool> UpdateCategoryAsync(int id, CreateCategoryRequest category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
