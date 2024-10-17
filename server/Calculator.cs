using System.Reflection.Metadata.Ecma335;

namespace server
{
    public class Calculator
    {
        public virtual float Add(float x, float y)
        {
            return x + y;
        }

        public virtual float Subtract(float x, float y) 
        { 
            return x - y;
        }

        public virtual float Multiply(float x, float y)
        {
            return x * y;
        }

        public virtual float Divide(float x, float y)
        {
            if (y == 0)
            {
                throw new DivideByZeroException("You cannot divide by zero");
            }
            return x / y;
        }
    }

}