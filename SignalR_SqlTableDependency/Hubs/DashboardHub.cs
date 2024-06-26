﻿using Microsoft.AspNetCore.SignalR;
using SignalR_SqlTableDependency.Repositories;

namespace SignalR_SqlTableDependency.Hubs
{
    public class DashboardHub : Hub
    {
        readonly ProductRepository productRepository;

        public DashboardHub(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            this.productRepository = new ProductRepository(connectionString);
        }

        public async Task SendProducts()
        {
            var products = productRepository.GetProducts();
            await Clients.All.SendAsync("ReceivedProducts", products);
        }
    }
}
