﻿using UnityEngine;
using System.Collections;
using UdpKit;

public class NullSocket : UdpPlatformSocket {
  NullPlatform platform;

  public NullSocket(NullPlatform p) {
    platform = p;
  }

  public override void Bind(UdpEndPoint ep) {

  }

  public override bool Broadcast {
    get;
    set;
  }

  public override void Close() {
  }

  public override UdpEndPoint EndPoint {
    get { return UdpEndPoint.Any; }
  }

  public override string Error {
    get { return ""; }
  }

  public override bool IsBound {
    get { return true; }
  }

  public override UdpPlatform Platform {
    get { return platform; }
  }

  public override int RecvFrom(byte[] buffer, int bufferSize, ref UdpEndPoint remoteEndpoint) {
    return 0;
  }

  public override bool RecvPoll(int timeout) {
    return false;
  }

  public override bool RecvPoll() {
    return false;
  }

  public override int SendTo(byte[] buffer, int bytesToSend, UdpEndPoint endpoint) {
    return bytesToSend;
  }
}
