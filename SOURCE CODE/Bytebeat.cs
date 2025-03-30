using System;
using System.Runtime.InteropServices;
using static Windows.APIs;
using System.Threading;

namespace Nemesis
{
    public class Bytebeats
    {
        static IntPtr hWaveOut;
        public static void Beat1()
        {
            while (true)
            {
                WAVEFORMATEX wfx = new WAVEFORMATEX
                {
                    wFormatTag = WAVE_FORMAT_PCM,
                    nChannels = 1,
                    nSamplesPerSec = 8000,
                    nAvgBytesPerSec = 8000,
                    nBlockAlign = 1,
                    wBitsPerSample = 8,
                    cbSize = 0
                };

                waveOutOpen(out hWaveOut, WAVE_MAPPER, ref wfx, NULL, NULL, CALLBACK_NULL);

                byte[] sbuffer = new byte[17000 * 60];

                for (int t = 0; t < sbuffer.Length; t++)
                {
                    sbuffer[t] = (byte)(10 * (t >> 7 | 3 * t | t >> (t >> 15)) + (t >> 8 & 5));
                }

                GCHandle handle = GCHandle.Alloc(sbuffer, GCHandleType.Pinned);


                WAVEHDR header = new WAVEHDR
                {
                    lpData = handle.AddrOfPinnedObject(),
                    dwBufferLength = (uint)sbuffer.Length,
                    dwFlags = 0,
                    dwLoops = 0,
                };

                waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
                waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
                waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

                try
                {
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadAbortException)
                {
                    handle.Free();
                    waveOutClose(hWaveOut);
                    waveOutReset(hWaveOut);
                }
            }
        }

        public static void Beat2()
        {
            while (true)
            {
                WAVEFORMATEX wfx = new WAVEFORMATEX
                {
                    wFormatTag = WAVE_FORMAT_PCM,
                    nChannels = 1,
                    nSamplesPerSec = 8000,
                    nAvgBytesPerSec = 8000,
                    nBlockAlign = 1,
                    wBitsPerSample = 8,
                    cbSize = 0
                };

                waveOutOpen(out hWaveOut, WAVE_MAPPER, ref wfx, NULL, NULL, CALLBACK_NULL);

                byte[] sbuffer = new byte[17000 * 60];

                for (int t = 0; t < sbuffer.Length; t++)
                {
                    sbuffer[t] = (byte)(t * ((t >> 12 | t >> 8) & 63 & t >> 4));
                }

                GCHandle handle = GCHandle.Alloc(sbuffer, GCHandleType.Pinned);


                WAVEHDR header = new WAVEHDR
                {
                    lpData = handle.AddrOfPinnedObject(),
                    dwBufferLength = (uint)sbuffer.Length,
                    dwFlags = 0,
                    dwLoops = 0,
                };

                waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
                waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
                waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

                try
                {
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadAbortException)
                {
                    handle.Free();
                    waveOutClose(hWaveOut);
                    waveOutReset(hWaveOut);
                }
            }
        }


        public static void Beat3()
        {
            while (true)
            {
                WAVEFORMATEX wfx = new WAVEFORMATEX
                {
                    wFormatTag = WAVE_FORMAT_PCM,
                    nChannels = 1,
                    nSamplesPerSec = 2000,
                    nAvgBytesPerSec = 2000,
                    nBlockAlign = 1,
                    wBitsPerSample = 8,
                    cbSize = 0
                };

                waveOutOpen(out hWaveOut, WAVE_MAPPER, ref wfx, NULL, NULL, CALLBACK_NULL);

                byte[] sbuffer = new byte[17000 * 60];

                for (int t = 0; t < sbuffer.Length; t++)
                {
                    sbuffer[t] = (byte)(3 * ((t >> 1) + 20) * t >> 14 * t >> 18);
                }

                GCHandle handle = GCHandle.Alloc(sbuffer, GCHandleType.Pinned);


                WAVEHDR header = new WAVEHDR
                {
                    lpData = handle.AddrOfPinnedObject(),
                    dwBufferLength = (uint)sbuffer.Length,
                    dwFlags = 0,
                    dwLoops = 0,
                };

                waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
                waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
                waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

                try
                {
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadAbortException)
                {
                    handle.Free();
                    waveOutClose(hWaveOut);
                    waveOutReset(hWaveOut);
                }
            }
        }

        public static void Beat4()
        {
            while (true)
            {
                WAVEFORMATEX wfx = new WAVEFORMATEX
                {
                    wFormatTag = WAVE_FORMAT_PCM,
                    nChannels = 1,
                    nSamplesPerSec = 8000,
                    nAvgBytesPerSec = 8000,
                    nBlockAlign = 1,
                    wBitsPerSample = 8,
                    cbSize = 0
                };

                waveOutOpen(out hWaveOut, WAVE_MAPPER, ref wfx, NULL, NULL, CALLBACK_NULL);

                byte[] sbuffer = new byte[17000 * 60];

                for (int t = 0; t < sbuffer.Length; t++)
                {
                    sbuffer[t] = (byte)(((t / 800 | 10) * t & 85) - t & t / 4);
                }

                GCHandle handle = GCHandle.Alloc(sbuffer, GCHandleType.Pinned);


                WAVEHDR header = new WAVEHDR
                {
                    lpData = handle.AddrOfPinnedObject(),
                    dwBufferLength = (uint)sbuffer.Length,
                    dwFlags = 0,
                    dwLoops = 0,
                };

                waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
                waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
                waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

                try
                {
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadAbortException)
                {
                    handle.Free();
                    waveOutClose(hWaveOut);
                    waveOutReset(hWaveOut);
                }
            }
        }

        public static void Beat5()
        {
            while (true)
            {
                WAVEFORMATEX wfx = new WAVEFORMATEX
                {
                    wFormatTag = WAVE_FORMAT_PCM,
                    nChannels = 1,
                    nSamplesPerSec = 8000,
                    nAvgBytesPerSec = 8000,
                    nBlockAlign = 1,
                    wBitsPerSample = 8,
                    cbSize = 0
                };

                waveOutOpen(out hWaveOut, WAVE_MAPPER, ref wfx, NULL, NULL, CALLBACK_NULL);

                byte[] sbuffer = new byte[17000 * 60];

                for (int t = 0; t < sbuffer.Length; t++)
                {
                    sbuffer[t] = (byte)(t * (t >> 30 | t >> 13) / 2 >> (t * (t >> 18 | t >> 10) & 7 ^ t >> 7 | t >> 18) | t / 16 >> (t & t / 6));
                }

                GCHandle handle = GCHandle.Alloc(sbuffer, GCHandleType.Pinned);


                WAVEHDR header = new WAVEHDR
                {
                    lpData = handle.AddrOfPinnedObject(),
                    dwBufferLength = (uint)sbuffer.Length,
                    dwFlags = 0,
                    dwLoops = 0,
                };

                waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
                waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
                waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

                try
                {
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadAbortException)
                {
                    handle.Free();
                    waveOutClose(hWaveOut);
                    waveOutReset(hWaveOut);
                }
            }
        }

        public static void Beat6()
        {
            while (true)
            {
                WAVEFORMATEX wfx = new WAVEFORMATEX
                {
                    wFormatTag = WAVE_FORMAT_PCM,
                    nChannels = 1,
                    nSamplesPerSec = 8000,
                    nAvgBytesPerSec = 8000,
                    nBlockAlign = 1,
                    wBitsPerSample = 8,
                    cbSize = 0
                };

                waveOutOpen(out hWaveOut, WAVE_MAPPER, ref wfx, NULL, NULL, CALLBACK_NULL);

                byte[] sbuffer = new byte[17000 * 60];

                for (int t = 0; t < sbuffer.Length; t++)
                {
                    sbuffer[t] = (byte)((t * 43532 >> 233) * (t >> 325) | t >> 2543);
                }

                GCHandle handle = GCHandle.Alloc(sbuffer, GCHandleType.Pinned);


                WAVEHDR header = new WAVEHDR
                {
                    lpData = handle.AddrOfPinnedObject(),
                    dwBufferLength = (uint)sbuffer.Length,
                    dwFlags = 0,
                    dwLoops = 0,
                };

                waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
                waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
                waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

                try
                {
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadAbortException)
                {
                    handle.Free();
                    waveOutClose(hWaveOut);
                    waveOutReset(hWaveOut);
                }
            }
        }

        public static void Beat7()
        {
            while (true)
            {
                WAVEFORMATEX wfx = new WAVEFORMATEX
                {
                    wFormatTag = WAVE_FORMAT_PCM,
                    nChannels = 1,
                    nSamplesPerSec = 6000,
                    nAvgBytesPerSec = 6000,
                    nBlockAlign = 1,
                    wBitsPerSample = 8,
                    cbSize = 0
                };

                waveOutOpen(out hWaveOut, WAVE_MAPPER, ref wfx, NULL, NULL, CALLBACK_NULL);

                byte[] sbuffer = new byte[17000 * 60];

                for (int t = 0; t < sbuffer.Length; t++)
                {
                    sbuffer[t] = (byte)(((((t >> 10 & 44) % 32 >> 1) + ((t >> 9 & 44) % 32 >> 1)) * (32768 > t % 65536 ? 1 : 4 / 5) * t | t >> 3) * (t | t >> 8 | t >> 6));
                }

                GCHandle handle = GCHandle.Alloc(sbuffer, GCHandleType.Pinned);


                WAVEHDR header = new WAVEHDR
                {
                    lpData = handle.AddrOfPinnedObject(),
                    dwBufferLength = (uint)sbuffer.Length,
                    dwFlags = 0,
                    dwLoops = 0,
                };

                waveOutPrepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
                waveOutWrite(hWaveOut, ref header, (uint)Marshal.SizeOf(header));
                waveOutUnprepareHeader(hWaveOut, ref header, (uint)Marshal.SizeOf(header));

                try
                {
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadAbortException)
                {
                    handle.Free();
                    waveOutClose(hWaveOut);
                    waveOutReset(hWaveOut);
                }
            }
        }
    }

}