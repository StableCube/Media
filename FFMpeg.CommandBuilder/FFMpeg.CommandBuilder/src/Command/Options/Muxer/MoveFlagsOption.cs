using System;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Muxer
{
        public enum FlagType
        {
            FragKeyframe,
            EmptyMoov,
            SeparateMoof,
            SkipSidx,
            FastStart,
            RtphInt,
            DisableChpl,
            OmitTfhdOffset,
            DefaultBaseMoof,
            NegativeCtsOffsets
        }

    public class MoveFlagsOption : MuxerOptionBase
    {
        public const string Key = "-movflags";

        public FlagType Flag { get; set; }

        public MoveFlagsOption() : base(Key)
        {
        }

        public override CommandParam GetCommandParameter()
        {
            string value = "";
            switch (Flag)
            {
                case FlagType.DefaultBaseMoof:
                    value = "default_base_moof";
                break;
                case FlagType.DisableChpl:
                    value = "disable_chpl";
                break;
                case FlagType.EmptyMoov:
                    value = "empty_moov";
                break;
                case FlagType.FastStart:
                    value = "+faststart";
                break;
                case FlagType.FragKeyframe:
                    value = "frag_keyframe";
                break;
                case FlagType.NegativeCtsOffsets:
                    value = "negative_cts_offsets";
                break;
                case FlagType.OmitTfhdOffset:
                    value = "omit_tfhd_offset";
                break;
                case FlagType.RtphInt:
                    value = "rtphint";
                break;
                case FlagType.SeparateMoof:
                    value = "separate_moof";
                break;
                case FlagType.SkipSidx:
                    value = "skip_sidx";
                break;
                default:
                    throw new ArgumentOutOfRangeException("MoveFlag: " + Flag.ToString());
            }

            return new CommandParam(OptionKey, value);
        }
    }
}