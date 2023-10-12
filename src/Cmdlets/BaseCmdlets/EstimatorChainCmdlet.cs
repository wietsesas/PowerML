using Microsoft.ML.Data;
using Microsoft.ML;
using System.Management.Automation;

namespace PowerML.Cmdlets
{
    /// <summary>
    /// The base class for transform and trainer cmdlets.
    /// </summary>
    public abstract class EstimatorChainCmdlet : ContextCmdlet
    {
        /// <summary>
        /// Append the created estimator to the end of this chain.
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        [ValidateNotNull()]
        [PSDefaultValue(Value = null)]
        public EstimatorChain<ITransformer>? AppendTo { get; set; } = null;

        /// <summary>
        /// The scope allows for 'tagging' the estimators (and subsequently transformers) in the chain to be used 'only for training', 'for training and evaluation' etc.
        /// </summary>
        [Parameter()]
        [PSDefaultValue(Value = TransformerScope.Everything)]
        public TransformerScope AppendScope { get; set; } = TransformerScope.Everything;

        /// <summary>
        /// Append a new estimator to the provided chain.
        /// </summary>
        /// <param name="estimator">The estimator to append</param>
        /// <returns>Returns an EstimatorChain object</returns>
        protected EstimatorChain<ITransformer> AppendEstimator(IEstimator<ITransformer> estimator)
        {
            AppendTo ??= new EstimatorChain<ITransformer>();
            return AppendTo.Append(estimator, AppendScope);
        }
    }
}
