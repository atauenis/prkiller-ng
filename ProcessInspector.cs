namespace prkiller_ng
{
	/// <summary>
	/// Process Inspector template
	/// </summary>
	public abstract class ProcessInspector
	{
		/// <summary>
		/// Does the Inspector can inspect the <paramref name="ProcessName"/> processes.
		/// </summary>
		/// <param name="ProcessName">Process name.</param>
		/// <returns><c>true</c> if the Inspector can.</returns>
		public abstract bool Applicable(string ProcessName);
		/// <summary>
		/// Inspect the process for details.
		/// </summary>
		/// <param name="PI">ProcessInfo instance.</param>
		/// <returns>Detailed info about the process, or <c>null</c> if it is not available.</returns>
		public abstract string Inspect(ProcessInfo PI);
	}
}
