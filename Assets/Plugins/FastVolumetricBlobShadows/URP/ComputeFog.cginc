void ComputeFog_float(float fogFactor, out float fogDistance)
{
	fogDistance = 1;
	#if defined(FOG_LINEAR) || defined(FOG_EXP) || defined(FOG_EXP2)
		#if defined(FOG_EXP)
			// factor = exp(-density*z)
			// fogFactor = density*z compute at vertex
			fogDistance = saturate(exp2(-fogFactor));
		#elif defined(FOG_EXP2)
			// factor = exp(-(density*z)^2)
			// fogFactor = density*z compute at vertex
			fogDistance = saturate(exp2(-fogFactor * fogFactor));
		#elif defined(FOG_LINEAR)
			fogDistance = fogFactor;
		#endif
	#endif
}