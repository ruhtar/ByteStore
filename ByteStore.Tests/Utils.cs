﻿using ByteStore.Domain.Aggregates;
using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Infrastructure;
using ByteStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ByteStore.Tests;

public abstract class Utils
{
    internal static AppDbContext GetDbContext()
    {
        var dbOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        return new AppDbContext(dbOptionsBuilder.Options);
    }

    // internal static ProductRepository GetProductRepository()
    // {
    //     var dbContext = GetDbContext();
    //     return new ProductRepository(dbContext);
    // }
    
    // internal static UserRepository GetUserRepository()
    // {
    //     var dbContext = GetDbContext();
    //     return new UserRepository(dbContext, GetShoppingCartRepository());
    // }
    
    internal static ShoppingCartRepository GetShoppingCartRepository()
    {
        var dbContext = GetDbContext();
        return new ShoppingCartRepository(dbContext);
    }

    internal static User GetUserMock()
    {
        return new User
        {
            UserId = 1,
            Username = "User",
            Password = "!123Qwe"
        };
    }

    internal static Address GetAddressMock()
    {
        return new Address
        {
            Street = "Rua",
            StreetNumber = 0,
            City = "Aracaju",
            State = "SE",
            Country = "Brasil"
        };
    }
    
    internal static UserAggregate GetUserAggregateMock(User user, Address address)
    {
        return new UserAggregate
        {
            UserAggregateId = 23123,
            User = user,
            Role = Roles.User,
            Address = address,
            ShoppingCart = null,
            PurchaseHistory = null
        };
    }

    internal static UserAggregate GetUserAggregateMock()
    {
        return new UserAggregate
        {
            UserAggregateId = 23123,
            User = GetUserMock(),
            Role = Roles.User,
            Address = GetAddressMock(),
            ShoppingCart = null,
            PurchaseHistory = null
        };
    }

    internal static List<Product> GetProductsMock()
    {
        return new List<Product>
        {
            new Product
            {
                ProductId = 1,
                ProductQuantity = 10,
                Name = "BOLA QUADRADA",
                Price = 999,
                ImageStorageUrl = "URL",
                Description = "SIM, UMA BOLA QUADRADA",
                Reviews = null,
                Rate = 5,
                TimesRated = 777
            },
            new Product
            {
                ProductId = 2,
                ProductQuantity = 20,
                Name = "RAQUETE DE AR",
                Price = new decimal(149.99),
                ImageStorageUrl = "URL_RAQUETE",
                Description = "PERFEITA PARA JOGAR EM AMBIENTES SEM GRAVIDADE",
                Reviews = null,
                Rate = 4,
                TimesRated = 543
            },
            new Product
            {
                ProductId = 3,
                ProductQuantity = 15,
                Name = "MEIAS ANTI-GRAVIDADE",
                Price = new decimal(29.99),
                ImageStorageUrl = "URL_MEIAS",
                Description = "VOE COM ESTILO USANDO NOSSAS MEIAS REVOLUCIONÁRIAS",
                Reviews = null,
                Rate = 4.5,
                TimesRated = 321
            },
            new Product
            {
                ProductId = 4,
                ProductQuantity = 5,
                Name = "CAPACETE DE NATAÇÃO COM VISÃO NOTURNA",
                Price = new decimal(79.99),
                ImageStorageUrl = "URL_CAPACETE",
                Description = "NADA COM SEGURANÇA MESMO NA ESCURIDÃO TOTAL",
                Reviews = null,
                Rate = 3.8,
                TimesRated = 432
            },
            new Product
            {
                ProductId = 5,
                ProductQuantity = 25,
                Name = "LUVA DE ESCREVER NO AR",
                Price = new decimal(49.99),
                ImageStorageUrl = "URL_LUVA",
                Description = "CRIE PALAVRAS NO AR COM NOSSA LUVA MÁGICA",
                Reviews = null,
                Rate = 4.2,
                TimesRated = 210
            },
            new Product
            {
                ProductId = 6,
                ProductQuantity = 8,
                Name = "CANETA AUTODESTRUTIVA",
                Price = new decimal(19.99),
                ImageStorageUrl = "URL_CANETA",
                Description = "ESCREVA MENSAGENS SECRETAS QUE SE AUTODESTRUÍRÃO",
                Reviews = null,
                Rate = 3.9,
                TimesRated = 298
            },
            // Adicione mais produtos conforme necessário
        };
    }
}