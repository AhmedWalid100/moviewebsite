namespace MoviesProject.Application.ResponseModels
{
    public class CreateResponse<T>
    {
        public bool isSuccess {  get; set; }
        public string message { get; set; }
        public T data { get; set; }


    }
}
