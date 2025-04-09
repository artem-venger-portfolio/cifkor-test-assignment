namespace WebClient
{
    public readonly struct DogBreedShortInfo
    {
        public DogBreedShortInfo(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}