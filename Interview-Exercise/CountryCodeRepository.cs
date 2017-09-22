namespace Interview_Exercise
{
    public class CountryCodeRepository : Repository
    {
        private Repository rep = new Repository();

        public override void Add(Country country)
        {
            rep.Add(country);
        }

        public override void Update(Country country)
        {
            rep.Update(country);
        }

        public override void Delete(string countryCode)
        {
            rep.Delete(countryCode);
        }

        public override Country Get(string countryCode)
        {
            return rep.Get(countryCode);
        }

        public override void Clear()
        {
            rep.Clear();
        }
    }
}
