//using Microsoft.EntityFrameworkCore;
//using MyBirds.Infrastructure.Database.Contexts;
//using MyBirds.Infrastructure.Domain.Entities;
//using MyBirds.Infrastructure.Domain.Repositories;

//namespace MyBirds.Infrastructure.Database.Repositories;

//internal class OrderRepository(AppDbContext appDbContext) : IOrderRepository
//{
//    private readonly AppDbContext _appDbContext = appDbContext;

//    public async Task<Order?> GetByNameAsync(string name)
//    {
//        return await _appDbContext.Orders.SingleOrDefaultAsync(o => o.Name == name);
//    }
//}
