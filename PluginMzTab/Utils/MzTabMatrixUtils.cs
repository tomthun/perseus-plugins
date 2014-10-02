﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using MzTabLibrary.model;
using PluginMzTab.extended;

namespace PluginMzTab.utils{
    internal class MzTabMatrixUtils{
        public static int[] GetSelectedIndices(List<string> selection, ItemCollection values){
            if (selection == null || values == null){
                return null;
            }

            int[] result = new int[selection.Count];
            for (int i = 0; i < selection.Count; i++){
                if (selection[i] == null){
                    continue;
                }

                result[i] = GetSelectedIndex(selection[i], values);
            }

            return result;
        }

        public static int GetSelectedIndex(string selection, IList<string> values){
            return selection == null || values == null ? -1 : values.IndexOf(selection);
        }

        public static int GetSelectedIndex(string selection, ItemCollection values){
            return selection == null || values == null ? -1 : values.IndexOf(selection);
        }

        public static IList<string> Sort(IList<string> values){
            if (values == null){
                return null;
            }

            return values is List<string>
                       ? SortList(values as List<string>)
                       : SortArray(values as string[]);
        }

        private static IList<string> SortArray(string[] values){
            if (values == null){
                return null;
            }
            Array.Sort(values);
            return values;
        }

        private static IList<string> SortList(List<string> values){
            if (values == null){
                return null;
            }
            values.Sort();
            return values;
        }

        public static int IndexOf(IList<Assay> assays, Assay assay){
            for (int i = 0; i < assays.Count; i++){
                if (Equals(assay, assays[i])){
                    return i;
                }
            }
            return -1;
        }

        private static bool Equals(SplitList<Param> param1, SplitList<Param> param2)
        {
            if (param1 == null && param2 == null)
            {
                return true;
            }

            if (param1 == null)
            {
                return false;
            }

            if (param2 == null)
            {
                return false;
            }

            if (param1.Count != param2.Count)
            {
                return false;
            }

            param1.Sort();
            param2.Sort();

            return !param1.Where((t, i) => !t.Equals(param2[i])).Any();
        }

        private static bool Equals(Param param1, Param param2){
            if (param1 == null && param2 == null){
                return true;
            }

            if (param1 == null){
                return false;
            }

            if (param2 == null){
                return false;
            }

            if (!param1.Equals(param2)){
                return false;
            }

            return true;
        }

        public static bool Equals(MsRunImpl run1, MsRunImpl run2)
        {
            if (run1 == null && run2 == null){
                return true;
            }

            if (run1 == null){
                return false;
            }

            if (run2 == null){
                return false;
            }


            if (!Equals(run1.Format, run2.Format)){
                return false;
            }

            if (!Equals(run1.IdFormat, run2.IdFormat)){
                return false;
            }

            if (!Equals(run1.FragmentationMethod, run2.FragmentationMethod)){
                return false;
            }

            if (!Equals(run1.FilePath, run2.FilePath)){
                return false;
            }

            return true;
        }

        public static bool Equals(Instrument instrument1, Instrument instrument2){
            if (instrument1 == null && instrument2 == null){
                return true;
            }

            if (instrument1 == null){
                return false;
            }

            if (instrument2 == null){
                return false;
            }


            if (!Equals(instrument1.Name, instrument2.Name)){
                return false;
            }

            if (!Equals(instrument1.Source, instrument2.Source)){
                return false;
            }

            if (!Equals(instrument1.Analyzer, instrument2.Analyzer)){
                return false;
            }

            if (!Equals(instrument1.Detector, instrument2.Detector)){
                return false;
            }

            return true;
        }

        public static bool Equals(Assay assay1, Assay assay2){
            if (assay1 == null && assay2 == null){
                return true;
            }

            if (assay1 == null){
                return false;
            }

            if (assay2 == null){
                return false;
            }


            if (!Equals(assay1.QuantificationReagent, assay2.QuantificationReagent)){
                return false;
            }

            return true;
        }

        public static bool Equals(Sample sample1, Sample sample2){
            if (sample1 == null && sample2 == null){
                return true;
            }

            if (sample1 == null){
                return false;
            }

            if (sample2 == null){
                return false;
            }

            if (!Equals(sample1.Description, sample2.Description)){
                return false;
            }

            if (!Equals(sample1.SpeciesList, sample2.SpeciesList)){
                return false;
            }

            if (!Equals(sample1.TissueList, sample2.TissueList)){
                return false;
            }

            if (!Equals(sample1.CellTypeList, sample2.CellTypeList)){
                return false;
            }

            if (!Equals(sample1.DiseaseList, sample2.DiseaseList)){
                return false;
            }

            return true;
        }

        public static IList<T> Unique<T>(IEnumerable<T> items){
            if (items == null){
                return null;
            }

            IList<T> unique = new List<T>();
            foreach (T item in items){
                if (unique.Count == 0){
                    unique.Add(item);
                    continue;
                }

                bool found = false;
                foreach (var r in unique){
                    if (r is MsRunImpl && item is MsRunImpl){
                        found = Equals(r as MsRunImpl, item as MsRunImpl);
                    }
                    if (r is Instrument && item is Instrument){
                        found = Equals(r as Instrument, item as Instrument);
                    }
                    if (r is Assay && item is Assay){
                        found = Equals(r as Assay, item as Assay);
                    }

                    if (found){
                        break;
                    }
                }

                if (!found){
                    unique.Add(item);
                }
            }
            return unique;
        }
    }
}