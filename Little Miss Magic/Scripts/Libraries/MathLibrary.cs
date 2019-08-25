using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathLibrary
{
    #region Numeric Spring

    // Reference: Ming-Lun "Allen" Chou
    // http://allenchou.net

    public static void Spring(ref float x, ref float v, float xt, float zeta, float omega, float h)
    {
        /*
          x         - value
          v         - velocity
          xt        - target value
          zeta      - damping ratio
          omega     - angular frequency
          h      - time step
        */ 

        float f = 1.0f + 2.0f * h * zeta * omega;
        float oo = omega * omega;
        float hoo = h * oo;
        float hhoo = h * hoo;
        float detInv = 1.0f / (f + hhoo);
        float detX = f * x + h * v + hhoo * xt;
        float detV = v + hoo * (xt - x);
        x = detX * detInv;
        v = detV * detInv;
    }

    public static void SpringSemiImplicitEuler(ref float x, ref float v, float xt, float zeta, float omega, float h)
    {
        /*
          x         - value
          v         - velocity
          xt        - target value
          zeta      - damping ratio
          omega     - angular frequency
          h      - time step
        */

        v += -2.0f * h * zeta * omega * v + h * omega * omega * (xt - x);
        x += h * v;
    }

    public static void SpringByHalfLife(ref float x, ref float v, float xt, float omega, float h, float lambda)
    {
        float zeta = -Mathf.Log(0.5f) / (omega * lambda);
        Spring(ref x, ref v, xt, zeta, omega, h);
    }

    #endregion
}
