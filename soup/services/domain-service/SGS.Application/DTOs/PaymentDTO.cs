namespace SGS.Application.DataTransferObjects;

/// <summary>
///   string partner_code =1;
///   string request_id =2;
///   string order_id =3; 
///   int64 amount =4; 
///   int64 response_time =5;
///   string message =6; 
///   int32 result_code =7; 
///   string pay_url =8; 
///   string deeplink =9; 
///   string qr_code_url =10;
///   string deeplink_web_in_app =11;
///  <summary>
public class CreateMomoResponseFromPaymentHub
{
    public string? PartnerCode { get; set; }
    public string? RequestId { get; set; }
    public string? OrderId { get; set; }
    public long Amount { get; set; }
    public long ResponseTime { get; set; }
    public string? Message { get; set; }
    public int ResultCode { get; set; }
    public string? PayUrl { get; set; }
    public string? Deeplink { get; set; }
    public string? QrCodeUrl { get; set; }
    public string? DeeplinkWebInApp { get; set; }
}

///   string order_id = 1;
///   int64 amount = 2;
///   string order_info =3;
///   string order_group_id = 4;
public record CreateMomoRequestToPaymentHub(
    string OrderId,
    long Amount,
    string OrderInfo,
    string? OrderGroupId,
    string? TransId
);