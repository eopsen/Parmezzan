﻿using System.ComponentModel.DataAnnotations;

namespace Parmezzan.Services.ShoppingCartAPI.Models.Dto
{
    public class CartHeaderDto
    {
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
    }
}
