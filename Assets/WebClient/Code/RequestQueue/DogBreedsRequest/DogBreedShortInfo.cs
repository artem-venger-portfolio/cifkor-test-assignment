namespace WebClient
{
    public readonly struct DogBreedShortInfo
    {
        public DogBreedShortInfo(string id, string name)
        {
            ID = id;
            Name = name;
        }

        public string ID { get; }
        public string Name { get; }
    }
}