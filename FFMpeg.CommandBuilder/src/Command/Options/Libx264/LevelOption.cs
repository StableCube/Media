
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Libx264
{
    public enum Level
    {
        L_1, L_1b, L_1_1, L_1_2, L_1_3,
        L_2, L_2_1, L_2_2,
        L_3, L_3_1, L_3_2,
        L_4, L_4_1, L_4_2,
        L_5, L_5_1, L_5_2,
        L_6, L_6_1, L_6_2,
    }

    public class LevelOption : Libx264OptionBase
    {
        public const string Key = "-level";

        ///<summary>Specify level (as defined by Annex A)</summary>
        ///<see>https://en.wikipedia.org/wiki/H.264/MPEG-4_AVC</see>
        public Level LevelValue { get; set; }

        public LevelOption() : base(Key, null) {}

        public LevelOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            string value = "1";
            switch (LevelValue)
            {
                case Level.L_1b:
                    value = "1b";
                    break;
                case Level.L_1_1:
                    value = "1.1";
                    break;
                case Level.L_1_2:
                    value = "1.2";
                    break;
                case Level.L_1_3:
                    value = "1.3";
                    break;
                case Level.L_2:
                    value = "2";
                    break;
                case Level.L_2_1:
                    value = "2.1";
                    break;
                case Level.L_2_2:
                    value = "2.2";
                    break;
                case Level.L_3:
                    value = "3";
                    break;
                case Level.L_3_1:
                    value = "3.1";
                    break;
                case Level.L_3_2:
                    value = "3.2";
                    break;
                case Level.L_4:
                    value = "4";
                    break;
                case Level.L_4_1:
                    value = "4.1";
                    break;
                case Level.L_4_2:
                    value = "4.2";
                    break;
                case Level.L_5:
                    value = "5";
                    break;
                case Level.L_5_1:
                    value = "5.1";
                    break;
                case Level.L_5_2:
                    value = "5.2";
                    break;
                case Level.L_6:
                    value = "6";
                    break;
                case Level.L_6_1:
                    value = "6.1";
                    break;
                case Level.L_6_2:
                    value = "6.2";
                    break;
            }

            return new CommandParam(StreamOptionKey, value);
        }
    }
}