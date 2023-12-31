﻿using AutoMapper;
using NadinSoftTask.Commands.Product;
using NadinSoftTask.DomainModel.Product;
using NadinSoftTask.DomainModel.Product.ValueObjects;

namespace NadinSoftTask.CommandHandlers
{
    public class ProductCommandMapper : Profile
    {
        public ProductCommandMapper()
        {
            CreateMap<CreateProductCommand, Product>()
            .ConstructUsing(src => new Product(
                src.Name,
                src.ProduceDate,
                src.ManufacturerPhoneNumber,
                src.ManufacturerEmail,
                src.IsAvailable,
                new OperatorInfo(src.CommandSender.UserId, src.CommandSender.Name)
            ));
        }
    }
}
