﻿using System;
using System.Linq;

namespace ZusiTcpInterface.Zusi3
{
  [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
  public abstract class EventRaisingZusiDataReceiverBase
  {
    private readonly DescriptorCollection _descriptorCollection;

    public EventRaisingZusiDataReceiverBase(DescriptorCollection descriptorCollection)
    {
      _descriptorCollection = descriptorCollection;
    }

    public event EventHandler<DataReceivedEventArgs<float>> FloatReceived;

    public event EventHandler<DataReceivedEventArgs<bool>> BoolReceived;

    protected void RaiseEventFor(CabDataChunkBase chunk)
    {
      if (RaiseEventIfChunkIs<float>(chunk, FloatReceived))
        return;

      if (RaiseEventIfChunkIs<bool>(chunk, BoolReceived))
        return;

      var payloadType = chunk.GetType().GenericTypeArguments.Single();
      throw new NotSupportedException(String.Format("The data type received ({0}) is not supported.", payloadType));
    }

    private bool RaiseEventIfChunkIs<T>(CabDataChunkBase chunk, EventHandler<DataReceivedEventArgs<T>> handler)
    {
      var dataChunk = chunk as CabDataChunk<T>;
      if (dataChunk == null) return false;

      var eventArgs = new DataReceivedEventArgs<T>(dataChunk.Payload, dataChunk.Id, _descriptorCollection[dataChunk.Id]);
      if (handler == null)
        return false;
      handler(this, eventArgs);

      return true;
    }
  }
}