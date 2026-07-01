using System;

namespace Application.DTOs;

public class ImageOrderDto
{
    public Guid ImageId { get; set; }
    public int DisplayOrder { get; set; }
}
