public class Port : ICloneable
{
    public int PortNumber { get; set; }

    public byte[] DataBuffer { get; set; } = new byte[64];

    public bool IsOpen { get; private set; }

    public string DeviceName { get; set; }

    public void Open()
    {
        IsOpen = true;
    }

    public void Close()
    {
        IsOpen = false;
    }

    public void Write(byte[] data)
    {
        if (!IsOpen)
            throw new Exception("Порт закритий");

        Array.Clear(DataBuffer, 0, DataBuffer.Length);

        Array.Copy(data, DataBuffer, Math.Min(data.Length, 64));
    }

    public byte[] Read()
    {
        if (!IsOpen)
            throw new Exception("Порт закритий");

        return DataBuffer;
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}