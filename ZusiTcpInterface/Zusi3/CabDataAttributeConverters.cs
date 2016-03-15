﻿using MiscUtil.Conversion;

namespace ZusiTcpInterface.Zusi3
{
  public static class CabDataAttributeConverters
  {
    private static readonly LittleEndianBitConverter BitConverter = EndianBitConverter.Little;

    public static IProtocolChunk ConvertSingle(short id, byte[] payload)
    {
      return new CabDataChunk<float>(id, BitConverter.ToSingle(payload, 0));
    }

    public static IProtocolChunk ConvertBoolAsSingle(short id, byte[] payload)
    {
      return new CabDataChunk<bool>(id, BitConverter.ToSingle(payload, 0) != 0f);
    }
  }
}