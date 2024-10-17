namespace server
{
    public class Die
    {
        private static Random rand = new Random();

        public virtual int Roll()
        {
            return rand.Next(1, 7);
        }
    }
}