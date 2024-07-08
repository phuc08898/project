

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdminTest.Models.Enums;

public interface IPagingResponse
{
    public int TotalRecords { get; }
    public int TotalPage { get; }
    public int Page { get; }
    public int OffSet { get; }
    public int? NextPage { get; }
    public int? PreviousPage { get; }
}

public interface IPagingRequest
{
    // muốn lấy bao nhiêu bản ghi cho từng base
    [Range(1, 1000)]
    [DefaultValue(100)]
    public int Limit { get; }

    // vị trí muốn lấy trong limit
    [Range(0, 999)]
    [DefaultValue(0)]
    public int Offset { get; }

    // số trang muốn lấy 
    [DefaultValue(1)]
    public int Page { get; }


}