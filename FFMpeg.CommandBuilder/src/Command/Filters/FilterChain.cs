using System;
using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    /// <summary>
    /// A filterchain consists of a sequence of connected filters, each one connected to the previous one in the sequence. 
    /// A filterchain is represented by a list of ","-separated filter descriptions. 
    /// </summary>
    public class FilterChain
    {
        public List<string> InputLinks { get; set; } = new List<string>();

        public List<string> OutputLinks { get; set; } = new List<string>();

        public List<IFFMpegFilter> Filters { get; set; } = new List<IFFMpegFilter>();

        public FilterChain()
        {
        }

        public FilterChain(params IFFMpegFilter[] filters)
        {
            foreach (var filter in filters)
            {
                if(filter == null)
                    throw new ArgumentNullException("filter");
            }

            Filters.AddRange(filters);
        }

        public void AddInputLink(StreamSpecifier inLink)
        {
            InputLinks.Add(inLink.ToString());
        }

        public void AddInputLink(string inLink)
        {
            InputLinks.Add(inLink);
        }

        public void AddOutputLink(StreamSpecifier outLink)
        {
            OutputLinks.Add(outLink.ToString());
        }

        public void AddOutputLink(string outLink)
        {
            OutputLinks.Add(outLink);
        }

        public void AddFilter(IFFMpegFilter filter)
        {
            if(filter == null)
                throw new ArgumentNullException("filter");

            Filters.Add(filter);
        }

        public void AddFilters(params IFFMpegFilter[] filters)
        {
            foreach (var filter in filters)
            {
                if(filter == null)
                    throw new ArgumentNullException("filter");
            }

            Filters.AddRange(filters);
        }

        public override string ToString()
        {
            List<string> filterStrings = new List<string>();
            string result = "";

            foreach (var filter in Filters)
            {
                filterStrings.Add(filter.ToString());
            }

            foreach (var inLink in InputLinks)
            {
                result += $"[{inLink}]";
            }

            result += String.Join(",", filterStrings);

            foreach (var outLink in OutputLinks)
            {
                result += $"[{outLink}]";
            }

            return result;
        }
    }
}