﻿namespace MySelf.MSACommerce.ElasticSyncWorker.Models;

public record SpuDetailDto
{
    public string Introduction { get; set; } = null!;
    public dynamic Parameter { get; set; } = null!;
    public dynamic Spec { get; set; } = null!;
}