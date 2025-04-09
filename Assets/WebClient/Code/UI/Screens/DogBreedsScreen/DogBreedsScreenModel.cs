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
        private DogBreedDescriptionRequest _currentDescriptionRequest;
        private int _breedIndexOfLoadingDescription;

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
        public event Action<int> DescriptionLoadingStarted;
        public event Action<int> DescriptionLoadingFinished;

        public void GetBreeds()
        {
            _requestQueue.Add(_dogBreedsRequestFactory.Create(DogBreadsReceivedEventHandler));
        }

        public void InterruptRequestsIfNeeded()
        {
            _requestQueue.Interrupt(RequestType.DogBreeds);
            InterruptDescriptionRequestIfNeeded();
        }
        
        private void InterruptDescriptionRequestIfNeeded()
        {
            if (_currentDescriptionRequest != null)
            {
                InvokeDescriptionLoadingFinished();
                RestDescriptionRequestFields();
                _requestQueue.Interrupt(RequestType.DogBreedDescription);
            }
        }

        public void GetBreedDescription(int index)
        {
            InterruptDescriptionRequestIfNeeded();

            _breedIndexOfLoadingDescription = index;
            var id = Breeds[_breedIndexOfLoadingDescription].ID;
            _currentDescriptionRequest = _descriptionRequestFactory.Create(id, DescriptionReceivedEventHandler);
            _requestQueue.Add(_currentDescriptionRequest);
            DescriptionLoadingStarted?.Invoke(_breedIndexOfLoadingDescription);
        }

        private void InvokeDescriptionLoadingFinished()
        {
            DescriptionLoadingFinished?.Invoke(_breedIndexOfLoadingDescription);
        }

        private void RestDescriptionRequestFields()
        {
            _currentDescriptionRequest = null;
            _breedIndexOfLoadingDescription = -1;
        }

        private void DescriptionReceivedEventHandler(DogBreedDescriptionRequest request)
        {
            InvokeDescriptionLoadingFinished();
            RestDescriptionRequestFields();
            DescriptionReceived?.Invoke(request.Result);
        }

        private void DogBreadsReceivedEventHandler(DogBreedsRequest request)
        {
            Breeds = request.Result;
            BreedsReceived?.Invoke();
        }
    }
}