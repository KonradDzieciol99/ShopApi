using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.IRepositories;
using Core.Products.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Products.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _photoService = photoService;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var createProductDto = request.CreateProductDto;
            const long MB = 1048576;
            if (createProductDto.Files.Count() == 0) { throw new NotImplementedException(); }
            foreach (var file in createProductDto.Files)
            {
                if (file.Length > (MB * 2))
                {
                    throw new NotImplementedException();
                }
            }

            List<Photo> photos = new List<Photo>();
            int index = 0;
            foreach (var file in createProductDto.Files)
            {

                var result = await _photoService.AddPhotoAsync(file);
                if (result.Error != null) { throw new NotImplementedException(); };
                Photo photo = new Photo
                {
                    Url = result.SecureUrl.AbsoluteUri,
                    PublicId = result.PublicId
                };
                if (index == createProductDto.mainPhotoIndex) { photo.IsMain = true; }
                photos.Add(photo);
                index++;
            }
            var product = _mapper.Map<Product>(createProductDto);

            product.BrandOfProduct = await _unitOfWork.BrandOfProductRepository.GetOneAsync(createProductDto.BrandOfProductId);
            product.CategoryOfProduct = await _unitOfWork.CategoryOfProductRepository.GetOneAsync(createProductDto.CategoryOfProductId);

            product.Photos = photos;

            _unitOfWork.ProductRepository.Add(product);
            if (await _unitOfWork.Complete())
            {
                //return CreatedAtAction(nameof(GetProduct), new { product.Id }, _mapper.Map<ProductDto>(product));
                return _mapper.Map<ProductDto>(product);
            }
            throw new BadRequestException("nie udało się stworzyć produktu");
        }
    }
}
