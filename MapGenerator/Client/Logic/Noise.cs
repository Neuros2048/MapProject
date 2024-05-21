namespace Client.Logic;

public class Noise
{
    
  

/* Function to linearly interpolate between a0 and a1
 * Weight w should be in the range [0.0, 1.0]
 */
    double interpolate(double a0, double a1, double w) {
    /* // You may want clamping by inserting:
     * if (0.0 > w) return a0;
     * if (1.0 < w) return a1;
     */
    return (a1 - a0) * w + a0;
    /* // Use this cubic interpolation [[Smoothstep]] instead, for a smooth appearance:
     * return (a1 - a0) * (3.0 - w * 2.0) * w * w + a0;
     *
     * // Use [[Smootherstep]] for an even smoother result with a second derivative equal to zero on boundaries:
     * return (a1 - a0) * ((w * (w * 6.0 - 15.0) + 10.0) * w * w * w) + a0;
     */
}

/* Create pseudorandom direction vector
 */
void randomGradient(uint ix, uint iy,out double vx, out double vy )  {
    // No precomputed gradients mean this works for any number of grid coordinates
    const int w = 32;
    const int s = 16;
    
    uint a = ix, b = iy;
    a *= 3284157443; b ^= a << s | a >> w-s;
    b *= 1911520717; a ^= b << s | b >> w-s;
    a *= 2048419325;
    double random = a * (3.14159265 / ~(~0u >> 1)); // in [0, 2*Pi]
    
    vx = Math.Cos(random);
    vy = Math.Sin(random);
    
}

// Computes the dot product of the distance and gradient vectors.
double dotGridGradient( uint ix, uint iy, double x, double y) {
    // Get gradient from integer coordinates
    double vx;
    double vy;
    randomGradient( ix, iy,out vx,out vy);

    // Compute the distance vector
    double dx = x - (double)ix;
    double dy = y - (double)iy;

    // Compute the dot-product
    return (dx*vx + dy*vy);
}


double perlin(double x, double y) {
    // Determine grid cell coordinates
    uint x0 = (uint) Math.Floor(x);
    uint x1 = x0 + 1;
    uint y0 = (uint)Math.Floor(y);
    uint y1 = y0 + 1;

    // Determine interpolation weights
    // Could also use higher order polynomial/s-curve here
    double sx = x - (double)x0;
    double sy = y - (double)y0;

    // Interpolate between grid point gradients
    double n0, n1, ix0, ix1, value;

    n0 = dotGridGradient(x0, y0, x, y);
    n1 = dotGridGradient(x1, y0, x, y);
    ix0 = interpolate(n0, n1, sx);

    n0 = dotGridGradient(x0, y1, x, y);
    n1 = dotGridGradient(x1, y1, x, y);
    ix1 = interpolate(n0, n1, sx);

    value = interpolate(ix0, ix1, sy);
    return value; // Will return in range -1 to 1. To make it in range 0 to 1, multiply by 0.5 and add 0.5
}

    private void MakeSeed(int n, int m, int seed, out double[] fSeed)
    {
        fSeed = new double[n * m];
        Random random = new Random(seed);
        for (int i = 0; i < n * m; i++)
        {
            fSeed[i] = random.NextDouble();
            
        }
    }

    public void PerlinNoise2D(int nWidth, int nHeight, int seed, int nOctaves, double fBias, out double[][] fOutput)
    {
        MakeSeed(nWidth,nHeight,seed,out var fSeed);
        fOutput = new double[nWidth][];

        for (int x = 0; x < nWidth; x++)
        {
            fOutput[x] = new double[nHeight];
            for (int y = 0; y < nHeight; y++)
            {
                double fNoise = 0.0f;
                double fScaleAcc = 0.0f;
                double fScale = 1.0f;

                for (int o = 0; o < nOctaves; o++)
                {
                    int nPitch = nWidth >> o;
                    int nSampleX1 = (x / nPitch) * nPitch;
                    int nSampleY1 = (y / nPitch) * nPitch;

                    int nSampleX2 = (nSampleX1 + nPitch) % nWidth;
                    int nSampleY2 = (nSampleY1 + nPitch) % nWidth;

                    double fBlendX = (double)(x - nSampleX1) / (double)nPitch;
                    double fBlendY = (double)(y - nSampleY1) / (double)nPitch;

                    double fSampleT = (1.0f - fBlendX) * fSeed[nSampleY1 * nWidth + nSampleX1] +
                                      fBlendX * fSeed[nSampleY1 * nWidth + nSampleX2];
                    double fSampleB = (1.0f - fBlendX) * fSeed[nSampleY2 * nWidth + nSampleX1] +
                                      fBlendX * fSeed[nSampleY2 * nWidth + nSampleX2];

                    fScaleAcc += fScale;
                    fNoise += (fBlendY * (fSampleB - fSampleT) + fSampleT) * fScale;
                    fScale = fScale / fBias;
                }

                // Scale to seed range
               
                fOutput[x][y] = fNoise / fScaleAcc;
            }
        }

    }
}