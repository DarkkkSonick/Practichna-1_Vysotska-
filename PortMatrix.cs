public class PortMatrix
{
    private Port[,] ports = new Port[16, 16];

    public PortMatrix()
    {
        int counter = 1;

        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                ports[i, j] = new Port
                {
                    PortNumber = counter++,
                    DeviceName = $"Device_{i}_{j}"
                };
            }
        }
    }

    public void OpenPort(int row, int col)
    {
        ports[row, col].Open();
    }

    public void ClosePort(int row, int col)
    {
        ports[row, col].Close();
    }

    public void WriteToPort(int row, int col, byte[] data)
    {
        ports[row, col].Write(data);
    }

    public byte[] ReadFromPort(int row, int col)
    {
        return ports[row, col].Read();
    }

    public void ScanMatrix()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                Console.Write(ports[i, j].IsOpen ? "[OPEN] " : "[CLOSED] ");
            }

            Console.WriteLine();
        }
    }

    public int CountOpenPorts()
    {
        int count = 0;

        foreach (var port in ports)
        {
            if (port.IsOpen)
                count++;
        }

        return count;
    }
}