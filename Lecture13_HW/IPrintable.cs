namespace Lecture13_HW
{
    internal interface IPrintable
    {
        void Print();

        void PrintDetailed()
        {
            // default-ად იგივე იყოს რაც Print
            Print();
        }
    }
}
