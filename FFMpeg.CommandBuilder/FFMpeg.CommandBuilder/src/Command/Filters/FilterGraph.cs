using System;
using System.Collections.Generic;
using StableCube.Media.FFMpeg.CommandBuilder.Options.Main;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    /// <summary>
    /// A filtergraph consists of a sequence of filterchains. 
    /// A sequence of filterchains is represented by a list of ";"-separated filterchain descriptions. 
    /// </summary>
    public class FilterGraph
    {
        public List<FilterChain> FilterChains { get; set; } = new List<FilterChain>();

        public FilterGraph()
        {
        }

        public FilterGraph(params FilterChain[] chains)
        {
            foreach (var chain in chains)
            {
                if(chain == null)
                    throw new ArgumentNullException("chain");
            }

            FilterChains.AddRange(chains);
        }

        public void AddChain(FilterChain chain)
        {
            if(chain == null)
                throw new ArgumentNullException("chain");

            FilterChains.Add(chain);
        }

        public void AddChains(params FilterChain[] chains)
        {
            foreach (var chain in chains)
            {
                if(chain == null)
                    throw new ArgumentNullException("chain");
            }
            
            FilterChains.AddRange(chains);
        }

        public override string ToString()
        {
            List<string> filterChainStrings = new List<string>();

            foreach (var chain in FilterChains)
            {
                filterChainStrings.Add(chain.ToString());
            }

            return String.Join(";", filterChainStrings);
        }

        /// <summary>
        /// Gets the command param for this complex filter graph
        /// </summary>
        public CommandParam GetCommandParameter()
        {
            var fcOpt = new FilterComplexOption()
            {
                Filter = this
            };

            return fcOpt.GetCommandParameter();
        }
    }
}