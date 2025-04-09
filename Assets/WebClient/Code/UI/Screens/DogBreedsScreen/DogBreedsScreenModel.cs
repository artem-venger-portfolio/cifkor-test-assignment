namespace WebClient
{
    public class DogBreedsScreenModel
    {
        private readonly DogBreedsRequest.Factory _dogBreedsRequestFactory;
        private readonly IRequestQueue _requestQueue;
        private readonly IProjectLogger _logger;

        public DogBreedsScreenModel(DogBreedsRequest.Factory dogBreedsRequestFactory, IRequestQueue requestQueue,
                                    IProjectLogger logger)
        {
            _dogBreedsRequestFactory = dogBreedsRequestFactory;
            _requestQueue = requestQueue;
            _logger = logger;
        }

        public void GetBreeds()
        {
            _requestQueue.Add(_dogBreedsRequestFactory.Create(DogBreadsReceivedEventHandler));
        }

        public void InterruptRequestsIfNeeded()
        {
            _requestQueue.Interrupt(RequestType.DogBreeds);
        }

        private void DogBreadsReceivedEventHandler(DogBreedsRequest request)
        {
            foreach (var info in request.Result)
            {
                _logger.LogInfo($"name: {info.Name}, id: {info.ID}");
            }
        }
    }
}