using System;
using System.Collections.Generic;

namespace WebClient
{
    public class DogBreedsScreenModel
    {
        private readonly DogBreedDescriptionRequest.Factory _descriptionRequestFactory;
        private readonly DogBreedsRequest.Factory _dogBreedsRequestFactory;
        private readonly IRequestQueue _requestQueue;
        private readonly IProjectLogger _logger;

        public DogBreedsScreenModel(DogBreedsRequest.Factory dogBreedsRequestFactory,
                                    DogBreedDescriptionRequest.Factory descriptionRequestFactory,
                                    IRequestQueue requestQueue, IProjectLogger logger)
        {
            _dogBreedsRequestFactory = dogBreedsRequestFactory;
            _descriptionRequestFactory = descriptionRequestFactory;
            _requestQueue = requestQueue;
            _logger = logger;
        }

        public List<DogBreedShortInfo> Breeds { get; private set; }

        public event Action BreedsReceived;
        public event Action<DogBreedDescription> DescriptionReceived;

        public void GetBreeds()
        {
            _requestQueue.Add(_dogBreedsRequestFactory.Create(DogBreadsReceivedEventHandler));
        }

        public void InterruptRequestsIfNeeded()
        {
            _requestQueue.Interrupt(RequestType.DogBreeds);
            _requestQueue.Interrupt(RequestType.DogBreedDescription);
        }

        public void GetBreedDescription(int index)
        {
            var id = Breeds[index].ID;
            var request = _descriptionRequestFactory.Create(id, DescriptionReceivedEventHandler);
            _requestQueue.Add(request);
        }

        private void DescriptionReceivedEventHandler(DogBreedDescriptionRequest request)
        {
            DescriptionReceived?.Invoke(request.Result);
        }

        private void DogBreadsReceivedEventHandler(DogBreedsRequest request)
        {
            Breeds = request.Result;
            BreedsReceived?.Invoke();
        }
    }
}