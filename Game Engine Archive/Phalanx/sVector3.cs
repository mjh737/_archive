using System;
using System.Collections.Generic;
using System.Text;

namespace Graphite
{
    public class sVector3
    {
        public float x;
        public float y;
        public float z;

        public sVector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public sVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Invert()
        {
            x = -x;
            y = -y;
            z = -z;
        }

        //Gets the Magnitude of this Vector
        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        //Gets the Squared Magnitude of this Vector
        public float SquaredMagnitude()
        {
            return (x * x + y * y + z * z);
        }

        //Normalizes this Vector
        public void Normalize()
        {
            //We want to divide x, y and z by the length to get a unit vector so multiply by the reciprocal
            
            float length = Magnitude(); 
            if (length > 0)
            {
                MultiplyByScalar(1 / Magnitude());
            }
        }

        //Multiply this Vector by a Scalar
        public void MultiplyByScalar(float scalar)
        {
            this.x = this.x * scalar;
            this.y = this.y * scalar;
            this.z = this.z * scalar;
        }

        //Multiply any Vector by a Scalar
        public static sVector3 operator *(sVector3 vector, float scalar)
        {
            return new sVector3(vector.x * scalar, vector.y * scalar, vector.z * scalar);
        }

        //Add a Vector to this Vector
        public void AddVector(sVector3 vector)
        {
            this.x += vector.x;
            this.y += vector.y;
            this.z += vector.z;
        }

        //Add two Vectors
        public static sVector3 operator +(sVector3 vector1, sVector3 vector2)
        {
            return new sVector3(vector1.x + vector2.x, vector1.y + vector2.y, vector1.z + vector2.z);
        }

        //Subtract a Vector from this Vector
        public void SubtractVector(sVector3 vector)
        {
            this.x -= vector.x;
            this.y -= vector.y;
            this.z -= vector.z;
        }

        //Subtract two Vectors
        public static sVector3 operator -(sVector3 vector1, sVector3 vector2)
        {
            return new sVector3(vector1.x - vector2.x, vector1.y - vector2.y, vector1.z - vector2.z);
        }

        //Add scaled vector
        public void AddScaledVector(sVector3 vector, float scalar)
        {
            this.x += vector.x * scalar;
            this.y += vector.y * scalar;
            this.z += vector.z * scalar;
        }

        //Returns the Component Product of this Vector and another Vector
        public sVector3 ComponentProduct(sVector3 vector)
        {
            return new sVector3(this.x * vector.x, this.y * vector.y, this.z * vector.z);
        }

        //Updates this Vector with the Component Product of this and another Vector
        public void UpdateAsComponentProduct(sVector3 vector)
        {
            this.x *= vector.x;
            this.y *= vector.y;
            this.z *= vector.z;
        }

        //Returns the dot product (scalar product) of this and another Vector
        public float DotProduct(sVector3 vector)
        {
            return this.x * vector.x + this.y * vector.y + this.z * vector.z;
        }

        //Returns the cross product (vector product) of this and another Vector
        public sVector3 CrossProduct(sVector3 vector)
        {
            return new sVector3(this.y * vector.z - this.z * vector.y, this.z * vector.x - this.x * vector.z, this.x * vector.y - this.y * vector.x);
        }

        //Updates this Vector with the Component Product of this and another Vector
        public void UpdateAsCrossProduct(sVector3 vector)
        {
            sVector3 crossProduct = this.CrossProduct(vector);

            this.x = crossProduct.x;
            this.y = crossProduct.y;
            this.z = crossProduct.z;
        }

        internal void Clear()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
