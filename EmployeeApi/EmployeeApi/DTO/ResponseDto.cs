namespace EmployeeApi.DTO
{
    public class ResponseDto
    {
        public Object? Result { get; set; }

        public Boolean IsSuccess { get; set; }=true;

        public string Message { get; set; } = "";
    }
}
