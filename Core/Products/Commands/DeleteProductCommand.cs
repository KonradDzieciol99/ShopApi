﻿using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Core.Products.Commands
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public DeleteProductCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
