using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Graphite
{
    public class xHeightMap
    {

        bool useHeightMask; public bool UseHeightMask { get { return useHeightMask; } set { useHeightMask = value; } }
        xHeightMask mask; public xHeightMask Mask { get { return mask; } set { mask = value; } }
        float maskedHeight; public float MaskedHeight { get { return maskedHeight; } set { maskedHeight = value; } }
        xFaultSettings faultSettings; public xFaultSettings FaultSettings { get { return faultSettings; } set { faultSettings = value; } }
        xMidPointSettings midPointSettings; public xMidPointSettings MidPointSettings { get { return midPointSettings; } set { midPointSettings = value; } }
        xParticleDepositSettings particleDepositSettings; public xParticleDepositSettings ParticleDepositSettings { get { return particleDepositSettings; } set { particleDepositSettings = value; } }
        xPerlinNoiseSettings perlinNoiseSettings; public xPerlinNoiseSettings PerlinNoiseSettings { get { return perlinNoiseSettings; } set { perlinNoiseSettings = value; } }

        //Default Flat Terrain HeightMap
        public xHeightMap()
        {
            this.width = 0;
            this.depth = 0;
            this.minHeight = 0.0f;
            this.maxHeight = 0.0f;

            this.heightData = null;
        }

        //Heighmap with external height data source
        public xHeightMap(int width, int depth, float minHeight, float maxHeight, float[] heightData)
        {
            this.width = width;
            this.depth = depth;
            this.minHeight = minHeight;
            this.maxHeight = maxHeight;

            this.heightData = (float[])heightData.Clone();
        }

        //HeightMap with masked height fields
        public xHeightMap(int width, int depth, float minHeight, float maxHeight, float maskedHeight)
        {
            this.width = width;
            this.depth = depth;
            this.minHeight = minHeight;
            this.maxHeight = maxHeight;
            this.maskedHeight = maskedHeight;

            this.heightData = new float[width * depth];
        }

        public float GetHeightValue(int x, int z)
        {
            return heightData[x + z * width];
        }

        public void SetHeightValue(int x, int z, float value)
        {
            if (value > maxHeight) value = maxHeight;
            if (value < minHeight) value = minHeight;

            heightData[x + z * width] = value;
        }

        public float GetHeightFactor(int x, int z)
        {
            return (heightData[x + z * width] / maxHeight);
        }

        public void GenerateRandomHeightmap()
        {
            Random random = new Random();

            for (int x = 0; x < width; ++x)
            {
                for (int z = 0; z < depth; ++z)
                {
                    heightData[x + z * width] = (float)random.NextDouble() * (maxHeight - minHeight) + minHeight;
                }
            }
        }

        public void GenerateRandomHeightMap(int width, int depth, float minHeight, float maxHeight)
        {
            Random random = new Random();

            this.width = width;
            this.depth = depth;
            this.minHeight = minHeight;
            this.maxHeight = maxHeight;

            heightData = new float[width * depth];

            GenerateRandomHeightmap();
        }

        public void GenerateFaultHeightmap(int width, int depth, float min, float max, xFaultSettings settings)
        {
            this.width = width;
            this.depth = depth;

            this.faultSettings = settings;

            this.heightData = new float[width * depth];

            GenerateFaultHeightmap();
        }

        public void GenerateFaultHeightmap()
        {
            int x1, z1, dx1, dz1;
            int x2, z2, dx2, dz2;

            int deltaHeight;

            for (int i = 0; i < faultSettings.Iterations; ++i)
            {
                // Calculate the deltaHeight for this iteration.
                // (linear interpolation from max delta to min delta).
                deltaHeight = faultSettings.MaxDelta - ((faultSettings.MaxDelta - faultSettings.MinDelta) * i) / faultSettings.Iterations;

                // Pick two random points on the field for the line.
                // (make sure they aren't identical).
                x1 = xRandomHelper.Random.Next(width);
                z1 = xRandomHelper.Random.Next(depth);

                do
                {
                    x2 = xRandomHelper.Random.Next(width);
                    z2 = xRandomHelper.Random.Next(depth);
                } while (x1 == x2 && z1 == z2);

                // dx1, dz1 is a vector in the direction of the line.
                dx1 = x2 - x1;
                dz1 = z2 - z1;

                for (x2 = 0; x2 < width; ++x2)
                {
                    for (z2 = 0; z2 < depth; ++z2)
                    {
                        // dx2, dz2 is a vector from x1, z1 to the candidate point.
                        dx2 = x2 - x1;
                        dz2 = z2 - z1;

                        // if y component of the cross product is 'up', then elevate this point.
                        if (dx2 * dz1 - dx1 * dz2 > 0)
                        {
                            heightData[x2 + width * z2] += (float)deltaHeight;
                        }
                    }
                }

                // Erode the terrain.
                if ((faultSettings.IterationsPerFilter != 0) && (i % faultSettings.IterationsPerFilter) == 0)
                {
                    FilterHeightmap(faultSettings.FilterValue);
                }
            }

            // Normalize heightmap (height field values in the range _minimumHeight - _maximumHeight.
            NormalizeHeightmap();
        }

        public void NormalizeHeightmap(float min, float max)
        {
            this.minHeight = min;
            this.maxHeight = max;

            NormalizeHeightmap();
        }

        public void NormalizeHeightmap()
        {
            float min = float.MaxValue;
            float max = float.MinValue;

            // Get the lowest and the highest values.
            for (int x = 0; x < width; ++x)
            {
                for (int z = 0; z < depth; ++z)
                {
                    if (heightData[x + z * width] > max)
                    {
                        max = heightData[x + z * width];
                    }

                    if (heightData[x + z * width] < min)
                    {
                        min = heightData[x + z * width];
                    }
                }
            }

            // If the heightmap is flat, we set it to the average between minimumHeight and maximumHeight.
            if (max <= min)
            {
                for (int x = 0; x < width; ++x)
                {
                    for (int z = 0; z < depth; ++z)
                    {
                        heightData[x + z * width] = (maxHeight - minHeight) * 0.5f;
                    }
                }
            }

            // Normalize the value between 0.0 and 1.0 then scale it between minimumHeight and maximumHeight.
            float diff = max - min;
            float scale = maxHeight - minHeight;

            for (int x = 0; x < width; ++x)
            {
                for (int z = 0; z < depth; ++z)
                {
                    heightData[x + z * width] = (heightData[x + z * width] - min) / diff * scale + minHeight;
                }
            }
        }

        public void FilterHeightmap(float filterValue)
        {
            // Erode rows left to right.
            for (int j = 0; j < depth; ++j)
            {
                for (int i = 1; i < width; ++i)
                {
                    heightData[i + j * width] = filterValue * heightData[i - 1 + j * width] + (1 - filterValue) * heightData[i + j * width];
                }
            }

            // Erode rows right to left.
            for (int j = 0; j < depth; ++j)
            {
                for (int i = 0; i < width - 1; ++i)
                {
                    heightData[i + j * width] = filterValue * heightData[i + 1 + j * width] + (1 - filterValue) * heightData[i + j * width];
                }
            }

            // Erode columns top to bottom.
            for (int j = 1; j < depth; ++j)
            {
                for (int i = 0; i < width; ++i)
                {
                    heightData[i + j * width] = filterValue * heightData[i + (j - 1) * width] + (1 - filterValue) * heightData[i + j * width];
                }
            }

            // Erode columns bottom to top.
            for (int j = 0; j < depth - 1; ++j)
            {
                for (int i = 0; i < width; ++i)
                {
                    heightData[i + j * width] = filterValue * heightData[i + (j + 1) * width] + (1 - filterValue) * heightData[i + j * width];
                }
            }
        }

        public void GenerateMidPointHeightmap(int wid, int dep, float min, float max, xMidPointSettings settings)
        {
            this.width = wid;
            this.depth = dep;

            midPointSettings = settings;

            heightData = new float[width * depth];

            GenerateMidPointHeightmap();
        }

        public void GenerateMidPointHeightmap()
        {
            int i, ni, mi, pmi;
            int j, nj, mj, pmj;
            int xwidth = width;
            int xdepth = depth;

            float deltaHeight = (float)width * 0.5f;

            float r = (float)Math.Pow(2.0f, -1 * midPointSettings.Rough);

            // Since the terrain wraps, all four corners are represented by the value at 0, 0.
            // So seeding the heightfield is very straightforward.
            heightData[0] = 1337.0f;

            while (xwidth > 0)
            {
                for (i = 0; i < xwidth; i += xwidth)
                {
                    for (j = 0; j < xdepth; j += xdepth)
                    {
                        ni = (i + xwidth) % width;
                        nj = (j + xdepth) % depth;

                        mi = (i + xwidth / 2);
                        mj = (j + xdepth / 2);

                        heightData[mi + width * mj] = (heightData[i + j * width] + heightData[ni + j * width] + heightData[i + nj * width] + heightData[ni + nj * width]) * 0.25f + xRandomHelper.GetFloatInRange(-deltaHeight * 0.5f, deltaHeight * 0.5f);
                    }
                }

                for (i = 0; i < width; i += xwidth)
                {
                    for (j = 0; j < depth; j += xdepth)
                    {
                        ni = (i + xwidth) % width;
                        nj = (j + xdepth) % depth;

                        mi = (i + xwidth / 2);
                        mj = (j + xdepth / 2);

                        pmi = (i - xwidth / 2 + width) % width;
                        pmj = (j - xdepth / 2 + depth) % depth;

                        // Calculate the square value for the top side of the rectangle.
                        heightData[mi + j * width] = (heightData[i + j * width] + heightData[ni + j * width] + heightData[mi + pmj * width] + heightData[mi + mj * width]) * 0.25f + xRandomHelper.GetFloatInRange(-deltaHeight * 0.5f, deltaHeight * 0.5f);

                        // Calculate the square value for the left side of the rectangle.
                        heightData[i + mj * width] = (heightData[i + j * width] + heightData[i + nj * width] + heightData[pmi + mj * width] + heightData[mi + mj * width]) * 0.25f + xRandomHelper.GetFloatInRange(-deltaHeight * 0.5f, deltaHeight * 0.5f);
                    }
                }

                // Set the values for the next iteration.
                xwidth /= 2;
                xdepth /= 2;
                deltaHeight *= r;
            }

            // Normalize heightmap (height field values in the range _minimumHeight - _maximumHeight.
            NormalizeHeightmap();
        }

        public void GenerateParticleDepositHeightmap(int width, int depth, float min, float max, xParticleDepositSettings settings)
        {
            this.width = width;
            this.depth = depth;

            this.particleDepositSettings = settings;

            heightData = new float[width * depth];

            GenerateParticleDepositHeightmap();
        }

        public void GenerateParticleDepositHeightmap()
        {
            int i, j, m, p, particleCount;
            int x, px, minx, maxx, sx, tx;
            int z, pz, minz, maxz, sz, tz;

            bool done;

            int[] dx = { 0, 1, 0, width - 1, 1, 1, width - 1, width - 1 };
            int[] dz = { 1, 0, depth - 1, 0, depth - 1, 1, depth - 1, 1 };

            float ch, ph;

            int[] calderaMap = new int[width * depth];

            // Clear the heightmap.
            for (i = 0; i < width * depth; ++i)
            {
                heightData[i] = 0.0f;
            }

            // For each jump ..
            for (p = 0; p < particleDepositSettings.Jumps; ++p)
            {
                // Pick a random spot.
                x = xRandomHelper.Random.Next(width);
                z = xRandomHelper.Random.Next(depth);

                // px and pz track where the caldera is formed.
                px = x;
                pz = z;

                // Determine how many particles we are going to drop.
                particleCount = xRandomHelper.Random.Next(particleDepositSettings.MinParticlesPerJump, particleDepositSettings.MaxParticlesPerJump);

                // Drop particles.
                for (i = 0; i < particleCount; ++i)
                {
                    // If we have to move the drop point, agitate it in a random direction.
                    if ((particleDepositSettings.PeakWalk != 0) && ((i % particleDepositSettings.PeakWalk) == 0))
                    {
                        m = xRandomHelper.Random.Next(8);

                        x = (x + dx[m] + width) % width;
                        z = (z + dz[m] + depth) % depth;
                    }

                    // Drop it.
                    heightData[x + z * width] += 1.0f;

                    // Now agitate it until it settles.
                    sx = x;
                    sz = z;

                    done = false;

                    // While it's not settled
                    while (!done)
                    {
                        // Consider it is.
                        done = true;

                        // Pick a random neighbor and start inspecting.
                        m = xRandomHelper.Random.Next();

                        for (j = 0; j < 8; ++j)
                        {
                            tx = (sx + dx[(j + m) % 8]) % width;
                            tz = (sz + dz[(j + m) % 8]) % depth;

                            // If we can move to this neighbor, do it.
                            if (heightData[tx + tz * width] + 1.0f < heightData[sx + sz * width])
                            {
                                heightData[tx + tz * width] += 1.0f;
                                heightData[sx + sz * width] -= 1.0f;

                                sx = tx;
                                sz = tz;

                                done = false;

                                break;
                            }
                        }
                    }

                    // Check to see if the latest point is higher than the caldera point.
                    // If so, move the caldera point here.
                    if (heightData[sx + sz * width] > heightData[px + pz * width])
                    {
                        px = sx;
                        pz = sz;
                    }
                }

                // Now that we are done with the peak, invert the caldera.
                //
                // ch is the caldera cutoff altitude.
                // ph is the height at the caldera start point.
                ph = heightData[px + pz * width];
                ch = ph * (1.0f - particleDepositSettings.Caldera);

                // We do a floodfill, so we use an array of integers to mark the visited locations.
                minx = px;
                maxx = px;
                minz = pz;
                maxz = pz;

                // Mark the start location for the caldera.
                calderaMap[px + pz * width] = 1;

                done = false;

                while (!done)
                {
                    // Assume work is done.
                    done = true;

                    sx = minx;
                    sz = minz;
                    tx = maxx;
                    tz = maxz;

                    // Examine the bounding rectangle looking for unvisited neighbors.
                    for (x = sx; x <= tx; ++x)
                    {
                        for (z = sz; z <= tz; ++z)
                        {
                            px = (x + width) % width;
                            pz = (z + depth) % depth;

                            // If this cell is marked but unvisited, check it out.
                            if (calderaMap[px + pz * width] == 1)
                            {
                                // Mark cell as visited.
                                calderaMap[px + pz * width] = 2;

                                // If this cell should be inverted, invert it and inspect neighbors.
                                // We mark any unmarked and unvisited neighbor.
                                // We don't invert any cells whose height exceeds the initial caldera height.
                                // This prevents small peaks from destroying large ones.
                                if ((heightData[px + pz * width] > ch) && (heightData[px + pz * width] <= ph))
                                {
                                    done = false;

                                    heightData[px + pz * width] = 2 * ch - heightData[px + pz * width];

                                    // Left and right neighbors.
                                    px = (px + 1) % width;

                                    if (calderaMap[px + pz * width] == 0)
                                    {
                                        if (x + 1 > maxx)
                                        {
                                            maxx = x + 1;
                                        }

                                        calderaMap[px + pz * width] = 1;
                                    }

                                    px = (px + width - 2) % width;

                                    if (calderaMap[px + pz * width] == 0)
                                    {
                                        if (x - 1 < minx)
                                        {
                                            minx = x - 1;
                                        }

                                        calderaMap[px + pz * width] = 1;
                                    }

                                    // Top and bottom neighbors.
                                    px = (x + width) % width;
                                    pz = (pz + 1) % depth;

                                    if (calderaMap[px + pz * width] == 0)
                                    {
                                        if (z + 1 > maxz)
                                        {
                                            maxz = z + 1;
                                        }

                                        calderaMap[px + pz * width] = 1;
                                    }

                                    pz = (pz + depth - 2) % depth;

                                    if (calderaMap[px + pz * width] == 0)
                                    {
                                        if (z - 1 < minz)
                                        {
                                            minz = z - 1;
                                        }

                                        calderaMap[px + pz * width] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Since calderas increase aliasing, we erode the terrain with a filter value proportional
            // to the prominence of the caldera.
            FilterHeightmap(particleDepositSettings.Caldera);

            // Normalize the heightmap.
            NormalizeHeightmap();
        }

        public void GeneratePerlinNoiseHeightmap(int width, int depth, float min, float max, xPerlinNoiseSettings settings)
        {
            this.width = width;
            this.depth = depth;

            this.perlinNoiseSettings = settings;

            heightData = new float[width * depth];

            GeneratePerlinNoiseHeightmap();
        }

        public void GeneratePerlinNoiseHeightmap()
        {
            int txi, tzi;

            float freq, amp;

            float xf, tx, fracx;
            float zf, tz, fracz;

            float v1, v2, v3, v4;
            float i1, i2, total;

            // For each height..
            for (int z = 0; z < depth; ++z)
            {
                for (int x = 0; x < width; ++x)
                {
                    // Scale x and y to the range of 0.0f, 1.0f.
                    xf = (float)x / (float)width;
                    zf = (float)z / (float)depth;

                    total = 0.0f;

                    // For each octaves..
                    for (int i = 0; i < perlinNoiseSettings.Octave; ++i)
                    {
                        // Calculate frequency and amplitude (different for each octave).
                        freq = (float)Math.Pow(2.0, i);
                        amp = (float)Math.Pow(perlinNoiseSettings.Persistence, i);

                        // Calculate the x, z noise coodinates.
                        tx = xf * freq;
                        tz = zf * freq;

                        txi = (int)tx;
                        tzi = (int)tz;

                        // Calculate the fractions of x and z.
                        fracx = tx - txi;
                        fracz = tz - tzi;

                        // Get noise per octave for these four points.
                        v1 = xRandomHelper.Noise(txi + tzi * 57 + perlinNoiseSettings.Seed);
                        v2 = xRandomHelper.Noise(txi + 1 + tzi * 57 + perlinNoiseSettings.Seed);
                        v3 = xRandomHelper.Noise(txi + (tzi + 1) * 57 + perlinNoiseSettings.Seed);
                        v4 = xRandomHelper.Noise(txi + 1 + (tzi + 1) * 57 + perlinNoiseSettings.Seed);

                        // Smooth noise in the x axis.
                        i1 = MathHelper.SmoothStep(v1, v2, fracx);
                        i2 = MathHelper.SmoothStep(v3, v4, fracx);

                        // Smooth in the z axis.
                        total += MathHelper.SmoothStep(i1, i2, fracz) * amp;
                    }

                    // Save to heightmap.
                    heightData[x + z * width] = total;
                }
            }

            // Normalize the terrain.
            NormalizeHeightmap();
        }

        public void CombineHeightmap(xHeightMap heightMap, float amount)
        {
            // Reject the heightmap if it doesn't fit.
            if ((width != heightMap.Width) ||
                (depth != heightMap.Depth) ||
                (minHeight != heightMap.MinHeight) ||
                (maxHeight != heightMap.MaxHeight))
            {
                return;
            }

            // Combine the heightmaps.
            // H1 = H1 * (1.0f - amount) + H2 * amount
            for (int x = 0; x < width; ++x)
            {
                for (int z = 0; z < depth; ++z)
                {
                    heightData[x + z * width] = heightData[x + z * width] * (1.0f - amount) + heightMap.GetHeightValue(x, z) * amount;
                }
            }
        }

        public void MultiplyHeightMap(xHeightMap heightmap)
        {
            // Reject the heightmap if it doesn't fit.
            if ((width != heightmap.Width) ||
                (depth != heightmap.Depth) ||
                (minHeight != heightmap.MinHeight) ||
                (maxHeight != heightmap.MaxHeight))
            {
                return;
            }

            // Multiply the heightmaps together.
            for (int x = 0; x < width; ++x)
            {
                for (int z = 0; z < depth; ++z)
                {
                    heightData[x + z * width] = heightData[x + z * width] * heightmap.GetHeightValue(x, z);
                }
            }
        }

        public void AddHeightmap(xHeightMap heightmap)
        {
            // Reject the heightmap if it doesn't fit.
            if ((width != heightmap.Width) ||
                (depth != heightmap.Depth) ||
                (minHeight != heightmap.MinHeight) ||
                (maxHeight != heightmap.MaxHeight))
            {
                return;
            }

            // Add the heightmaps together.
            for (int x = 0; x < width; ++x)
            {
                for (int z = 0; z < depth; ++z)
                {
                    heightData[x + z * width] = heightData[x + z * width] + heightmap.GetHeightValue(x, z);
                }
            }
        }

        /// <summary>
        /// Apply a mask on the heightmap.
        /// </summary>
        /// <param name="heightmask"></param>
        public void ApplyMask(xHeightMask heightmask)
        {
            // Reject the heightmask if it doesn't fit.
            if ((width != heightmask.Width) ||
                (depth != heightmask.Depth))
            {
                return;
            }

            // Apply the heightmask.
            for (int x = 0; x < width; ++x)
            {
                for (int z = 0; z < depth; ++z)
                {
                    heightData[x + z * width] = (heightData[x + z * width] - minHeight) * heightmask.GetMask(x, z) + minHeight;
                }
            }
        }



        public void SaveToRawFormat(String path)
        {
            try
            {
                FileStream stream = File.Open(path, FileMode.OpenOrCreate);

                if (stream != null)
                {
                    BinaryWriter writer = new BinaryWriter(stream);

                    writer.Write(width);
                    writer.Write(depth);

                    writer.Write(minHeight);
                    writer.Write(maxHeight);

                    for (int i = 0; i < heightData.Length; ++i)
                    {
                        writer.Write(heightData[i]);
                    }

                    writer.Flush();

                    stream.Close();
                }
            }
            catch (Exception)
            {

            }
        }

        public void LoadFromRawFormat(String path)
        {
            try
            {
                FileStream stream = File.Open(path, FileMode.Open);
                BinaryReader reader = new BinaryReader(stream);

                width = reader.ReadInt32();
                depth = reader.ReadInt32();

                minHeight = reader.ReadSingle();
                maxHeight = reader.ReadSingle();

                heightData = new float[width * depth];

                for (int i = 0; i < heightData.Length; ++i)
                {
                    heightData[i] = reader.ReadSingle();
                }

                reader.Close();

                stream.Close();
            }
            catch (Exception)
            {

            }
        }

        public object Clone()
        {
            xHeightMap heightMap = new xHeightMap(width, depth, minHeight, maxHeight, (float[])heightData.Clone());

            return (object)heightMap;
        }
    }
}
