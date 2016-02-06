
using System;
using DD.Collections.ICodeSet;
    
namespace DD.Text.UniCode
{
    public static class Block
    {
        
        #region Imported Header

// # Blocks-5.0.0.txt
// # Date: 2006-02-15, 15:40:00 [KW]
// #
// # Unicode Character Database
// # Copyright (c) 1991-2006 Unicode, Inc.
// # For terms of use, see http://www.unicode.org/terms_of_use.html
// # For documentation, see UCD.html
// #
// # Note:   The casing of block names is not normative.
// #         For example, "Basic Latin" and "BASIC LATIN" are equivalent.
// #
// # Format:
// # Start Code..End Code; Block Name

// # ================================================

// # Note:   When comparing block names, casing, whitespace, hyphens,
// #         and underbars are ignored.
// #         For example, "Latin Extended-A" and "latin extended a" are equivalent.
// #         For more information on the comparison of property values, 
// #            see UCD.html.
// #
// #  All code points not explicitly listed for Block
// #  have the value No_Block.

// # Property:	Block
// #
// # @missing: 0000..10FFFF; No_Block

        #endregion

        #region Properties

        /// <summary>
        /// 0000..007F; Basic Latin
        /// </summary>
        public static ICodeSet BasicLatin {
            get {
                if (_basicLatin_ == null) {
                    _basicLatin_ = ((Code)0x0000).Range (0x007F);
                }
                return _basicLatin_;
            }
        }
        private static ICodeSet _basicLatin_;

        /// <summary>
        /// 0080..00FF; Latin-1 Supplement
        /// </summary>
        public static ICodeSet Latin1Supplement {
            get {
                if (_latin1Supplement_ == null) {
                    _latin1Supplement_ = ((Code)0x0080).Range (0x00FF);
                }
                return _latin1Supplement_;
            }
        }
        private static ICodeSet _latin1Supplement_;

        /// <summary>
        /// 0100..017F; Latin Extended-A
        /// </summary>
        public static ICodeSet LatinExtendedA {
            get {
                if (_latinExtendedA_ == null) {
                    _latinExtendedA_ = ((Code)0x0100).Range (0x017F);
                }
                return _latinExtendedA_;
            }
        }
        private static ICodeSet _latinExtendedA_;

        /// <summary>
        /// 0180..024F; Latin Extended-B
        /// </summary>
        public static ICodeSet LatinExtendedB {
            get {
                if (_latinExtendedB_ == null) {
                    _latinExtendedB_ = ((Code)0x0180).Range (0x024F);
                }
                return _latinExtendedB_;
            }
        }
        private static ICodeSet _latinExtendedB_;

        /// <summary>
        /// 0250..02AF; IPA Extensions
        /// </summary>
        public static ICodeSet IPAExtensions {
            get {
                if (_iPAExtensions_ == null) {
                    _iPAExtensions_ = ((Code)0x0250).Range (0x02AF);
                }
                return _iPAExtensions_;
            }
        }
        private static ICodeSet _iPAExtensions_;

        /// <summary>
        /// 02B0..02FF; Spacing Modifier Letters
        /// </summary>
        public static ICodeSet SpacingModifierLetters {
            get {
                if (_spacingModifierLetters_ == null) {
                    _spacingModifierLetters_ = ((Code)0x02B0).Range (0x02FF);
                }
                return _spacingModifierLetters_;
            }
        }
        private static ICodeSet _spacingModifierLetters_;

        /// <summary>
        /// 0300..036F; Combining Diacritical Marks
        /// </summary>
        public static ICodeSet CombiningDiacriticalMarks {
            get {
                if (_combiningDiacriticalMarks_ == null) {
                    _combiningDiacriticalMarks_ = ((Code)0x0300).Range (0x036F);
                }
                return _combiningDiacriticalMarks_;
            }
        }
        private static ICodeSet _combiningDiacriticalMarks_;

        /// <summary>
        /// 0370..03FF; Greek and Coptic
        /// </summary>
        public static ICodeSet GreekandCoptic {
            get {
                if (_greekandCoptic_ == null) {
                    _greekandCoptic_ = ((Code)0x0370).Range (0x03FF);
                }
                return _greekandCoptic_;
            }
        }
        private static ICodeSet _greekandCoptic_;

        /// <summary>
        /// 0400..04FF; Cyrillic
        /// </summary>
        public static ICodeSet Cyrillic {
            get {
                if (_cyrillic_ == null) {
                    _cyrillic_ = ((Code)0x0400).Range (0x04FF);
                }
                return _cyrillic_;
            }
        }
        private static ICodeSet _cyrillic_;

        /// <summary>
        /// 0500..052F; Cyrillic Supplement
        /// </summary>
        public static ICodeSet CyrillicSupplement {
            get {
                if (_cyrillicSupplement_ == null) {
                    _cyrillicSupplement_ = ((Code)0x0500).Range (0x052F);
                }
                return _cyrillicSupplement_;
            }
        }
        private static ICodeSet _cyrillicSupplement_;

        /// <summary>
        /// 0530..058F; Armenian
        /// </summary>
        public static ICodeSet Armenian {
            get {
                if (_armenian_ == null) {
                    _armenian_ = ((Code)0x0530).Range (0x058F);
                }
                return _armenian_;
            }
        }
        private static ICodeSet _armenian_;

        /// <summary>
        /// 0590..05FF; Hebrew
        /// </summary>
        public static ICodeSet Hebrew {
            get {
                if (_hebrew_ == null) {
                    _hebrew_ = ((Code)0x0590).Range (0x05FF);
                }
                return _hebrew_;
            }
        }
        private static ICodeSet _hebrew_;

        /// <summary>
        /// 0600..06FF; Arabic
        /// </summary>
        public static ICodeSet Arabic {
            get {
                if (_arabic_ == null) {
                    _arabic_ = ((Code)0x0600).Range (0x06FF);
                }
                return _arabic_;
            }
        }
        private static ICodeSet _arabic_;

        /// <summary>
        /// 0700..074F; Syriac
        /// </summary>
        public static ICodeSet Syriac {
            get {
                if (_syriac_ == null) {
                    _syriac_ = ((Code)0x0700).Range (0x074F);
                }
                return _syriac_;
            }
        }
        private static ICodeSet _syriac_;

        /// <summary>
        /// 0750..077F; Arabic Supplement
        /// </summary>
        public static ICodeSet ArabicSupplement {
            get {
                if (_arabicSupplement_ == null) {
                    _arabicSupplement_ = ((Code)0x0750).Range (0x077F);
                }
                return _arabicSupplement_;
            }
        }
        private static ICodeSet _arabicSupplement_;

        /// <summary>
        /// 0780..07BF; Thaana
        /// </summary>
        public static ICodeSet Thaana {
            get {
                if (_thaana_ == null) {
                    _thaana_ = ((Code)0x0780).Range (0x07BF);
                }
                return _thaana_;
            }
        }
        private static ICodeSet _thaana_;

        /// <summary>
        /// 07C0..07FF; NKo
        /// </summary>
        public static ICodeSet NKo {
            get {
                if (_nKo_ == null) {
                    _nKo_ = ((Code)0x07C0).Range (0x07FF);
                }
                return _nKo_;
            }
        }
        private static ICodeSet _nKo_;

        /// <summary>
        /// 0900..097F; Devanagari
        /// </summary>
        public static ICodeSet Devanagari {
            get {
                if (_devanagari_ == null) {
                    _devanagari_ = ((Code)0x0900).Range (0x097F);
                }
                return _devanagari_;
            }
        }
        private static ICodeSet _devanagari_;

        /// <summary>
        /// 0980..09FF; Bengali
        /// </summary>
        public static ICodeSet Bengali {
            get {
                if (_bengali_ == null) {
                    _bengali_ = ((Code)0x0980).Range (0x09FF);
                }
                return _bengali_;
            }
        }
        private static ICodeSet _bengali_;

        /// <summary>
        /// 0A00..0A7F; Gurmukhi
        /// </summary>
        public static ICodeSet Gurmukhi {
            get {
                if (_gurmukhi_ == null) {
                    _gurmukhi_ = ((Code)0x0A00).Range (0x0A7F);
                }
                return _gurmukhi_;
            }
        }
        private static ICodeSet _gurmukhi_;

        /// <summary>
        /// 0A80..0AFF; Gujarati
        /// </summary>
        public static ICodeSet Gujarati {
            get {
                if (_gujarati_ == null) {
                    _gujarati_ = ((Code)0x0A80).Range (0x0AFF);
                }
                return _gujarati_;
            }
        }
        private static ICodeSet _gujarati_;

        /// <summary>
        /// 0B00..0B7F; Oriya
        /// </summary>
        public static ICodeSet Oriya {
            get {
                if (_oriya_ == null) {
                    _oriya_ = ((Code)0x0B00).Range (0x0B7F);
                }
                return _oriya_;
            }
        }
        private static ICodeSet _oriya_;

        /// <summary>
        /// 0B80..0BFF; Tamil
        /// </summary>
        public static ICodeSet Tamil {
            get {
                if (_tamil_ == null) {
                    _tamil_ = ((Code)0x0B80).Range (0x0BFF);
                }
                return _tamil_;
            }
        }
        private static ICodeSet _tamil_;

        /// <summary>
        /// 0C00..0C7F; Telugu
        /// </summary>
        public static ICodeSet Telugu {
            get {
                if (_telugu_ == null) {
                    _telugu_ = ((Code)0x0C00).Range (0x0C7F);
                }
                return _telugu_;
            }
        }
        private static ICodeSet _telugu_;

        /// <summary>
        /// 0C80..0CFF; Kannada
        /// </summary>
        public static ICodeSet Kannada {
            get {
                if (_kannada_ == null) {
                    _kannada_ = ((Code)0x0C80).Range (0x0CFF);
                }
                return _kannada_;
            }
        }
        private static ICodeSet _kannada_;

        /// <summary>
        /// 0D00..0D7F; Malayalam
        /// </summary>
        public static ICodeSet Malayalam {
            get {
                if (_malayalam_ == null) {
                    _malayalam_ = ((Code)0x0D00).Range (0x0D7F);
                }
                return _malayalam_;
            }
        }
        private static ICodeSet _malayalam_;

        /// <summary>
        /// 0D80..0DFF; Sinhala
        /// </summary>
        public static ICodeSet Sinhala {
            get {
                if (_sinhala_ == null) {
                    _sinhala_ = ((Code)0x0D80).Range (0x0DFF);
                }
                return _sinhala_;
            }
        }
        private static ICodeSet _sinhala_;

        /// <summary>
        /// 0E00..0E7F; Thai
        /// </summary>
        public static ICodeSet Thai {
            get {
                if (_thai_ == null) {
                    _thai_ = ((Code)0x0E00).Range (0x0E7F);
                }
                return _thai_;
            }
        }
        private static ICodeSet _thai_;

        /// <summary>
        /// 0E80..0EFF; Lao
        /// </summary>
        public static ICodeSet Lao {
            get {
                if (_lao_ == null) {
                    _lao_ = ((Code)0x0E80).Range (0x0EFF);
                }
                return _lao_;
            }
        }
        private static ICodeSet _lao_;

        /// <summary>
        /// 0F00..0FFF; Tibetan
        /// </summary>
        public static ICodeSet Tibetan {
            get {
                if (_tibetan_ == null) {
                    _tibetan_ = ((Code)0x0F00).Range (0x0FFF);
                }
                return _tibetan_;
            }
        }
        private static ICodeSet _tibetan_;

        /// <summary>
        /// 1000..109F; Myanmar
        /// </summary>
        public static ICodeSet Myanmar {
            get {
                if (_myanmar_ == null) {
                    _myanmar_ = ((Code)0x1000).Range (0x109F);
                }
                return _myanmar_;
            }
        }
        private static ICodeSet _myanmar_;

        /// <summary>
        /// 10A0..10FF; Georgian
        /// </summary>
        public static ICodeSet Georgian {
            get {
                if (_georgian_ == null) {
                    _georgian_ = ((Code)0x10A0).Range (0x10FF);
                }
                return _georgian_;
            }
        }
        private static ICodeSet _georgian_;

        /// <summary>
        /// 1100..11FF; Hangul Jamo
        /// </summary>
        public static ICodeSet HangulJamo {
            get {
                if (_hangulJamo_ == null) {
                    _hangulJamo_ = ((Code)0x1100).Range (0x11FF);
                }
                return _hangulJamo_;
            }
        }
        private static ICodeSet _hangulJamo_;

        /// <summary>
        /// 1200..137F; Ethiopic
        /// </summary>
        public static ICodeSet Ethiopic {
            get {
                if (_ethiopic_ == null) {
                    _ethiopic_ = ((Code)0x1200).Range (0x137F);
                }
                return _ethiopic_;
            }
        }
        private static ICodeSet _ethiopic_;

        /// <summary>
        /// 1380..139F; Ethiopic Supplement
        /// </summary>
        public static ICodeSet EthiopicSupplement {
            get {
                if (_ethiopicSupplement_ == null) {
                    _ethiopicSupplement_ = ((Code)0x1380).Range (0x139F);
                }
                return _ethiopicSupplement_;
            }
        }
        private static ICodeSet _ethiopicSupplement_;

        /// <summary>
        /// 13A0..13FF; Cherokee
        /// </summary>
        public static ICodeSet Cherokee {
            get {
                if (_cherokee_ == null) {
                    _cherokee_ = ((Code)0x13A0).Range (0x13FF);
                }
                return _cherokee_;
            }
        }
        private static ICodeSet _cherokee_;

        /// <summary>
        /// 1400..167F; Unified Canadian Aboriginal Syllabics
        /// </summary>
        public static ICodeSet UnifiedCanadianAboriginalSyllabics {
            get {
                if (_unifiedCanadianAboriginalSyllabics_ == null) {
                    _unifiedCanadianAboriginalSyllabics_ = ((Code)0x1400).Range (0x167F);
                }
                return _unifiedCanadianAboriginalSyllabics_;
            }
        }
        private static ICodeSet _unifiedCanadianAboriginalSyllabics_;

        /// <summary>
        /// 1680..169F; Ogham
        /// </summary>
        public static ICodeSet Ogham {
            get {
                if (_ogham_ == null) {
                    _ogham_ = ((Code)0x1680).Range (0x169F);
                }
                return _ogham_;
            }
        }
        private static ICodeSet _ogham_;

        /// <summary>
        /// 16A0..16FF; Runic
        /// </summary>
        public static ICodeSet Runic {
            get {
                if (_runic_ == null) {
                    _runic_ = ((Code)0x16A0).Range (0x16FF);
                }
                return _runic_;
            }
        }
        private static ICodeSet _runic_;

        /// <summary>
        /// 1700..171F; Tagalog
        /// </summary>
        public static ICodeSet Tagalog {
            get {
                if (_tagalog_ == null) {
                    _tagalog_ = ((Code)0x1700).Range (0x171F);
                }
                return _tagalog_;
            }
        }
        private static ICodeSet _tagalog_;

        /// <summary>
        /// 1720..173F; Hanunoo
        /// </summary>
        public static ICodeSet Hanunoo {
            get {
                if (_hanunoo_ == null) {
                    _hanunoo_ = ((Code)0x1720).Range (0x173F);
                }
                return _hanunoo_;
            }
        }
        private static ICodeSet _hanunoo_;

        /// <summary>
        /// 1740..175F; Buhid
        /// </summary>
        public static ICodeSet Buhid {
            get {
                if (_buhid_ == null) {
                    _buhid_ = ((Code)0x1740).Range (0x175F);
                }
                return _buhid_;
            }
        }
        private static ICodeSet _buhid_;

        /// <summary>
        /// 1760..177F; Tagbanwa
        /// </summary>
        public static ICodeSet Tagbanwa {
            get {
                if (_tagbanwa_ == null) {
                    _tagbanwa_ = ((Code)0x1760).Range (0x177F);
                }
                return _tagbanwa_;
            }
        }
        private static ICodeSet _tagbanwa_;

        /// <summary>
        /// 1780..17FF; Khmer
        /// </summary>
        public static ICodeSet Khmer {
            get {
                if (_khmer_ == null) {
                    _khmer_ = ((Code)0x1780).Range (0x17FF);
                }
                return _khmer_;
            }
        }
        private static ICodeSet _khmer_;

        /// <summary>
        /// 1800..18AF; Mongolian
        /// </summary>
        public static ICodeSet Mongolian {
            get {
                if (_mongolian_ == null) {
                    _mongolian_ = ((Code)0x1800).Range (0x18AF);
                }
                return _mongolian_;
            }
        }
        private static ICodeSet _mongolian_;

        /// <summary>
        /// 1900..194F; Limbu
        /// </summary>
        public static ICodeSet Limbu {
            get {
                if (_limbu_ == null) {
                    _limbu_ = ((Code)0x1900).Range (0x194F);
                }
                return _limbu_;
            }
        }
        private static ICodeSet _limbu_;

        /// <summary>
        /// 1950..197F; Tai Le
        /// </summary>
        public static ICodeSet TaiLe {
            get {
                if (_taiLe_ == null) {
                    _taiLe_ = ((Code)0x1950).Range (0x197F);
                }
                return _taiLe_;
            }
        }
        private static ICodeSet _taiLe_;

        /// <summary>
        /// 1980..19DF; New Tai Lue
        /// </summary>
        public static ICodeSet NewTaiLue {
            get {
                if (_newTaiLue_ == null) {
                    _newTaiLue_ = ((Code)0x1980).Range (0x19DF);
                }
                return _newTaiLue_;
            }
        }
        private static ICodeSet _newTaiLue_;

        /// <summary>
        /// 19E0..19FF; Khmer Symbols
        /// </summary>
        public static ICodeSet KhmerSymbols {
            get {
                if (_khmerSymbols_ == null) {
                    _khmerSymbols_ = ((Code)0x19E0).Range (0x19FF);
                }
                return _khmerSymbols_;
            }
        }
        private static ICodeSet _khmerSymbols_;

        /// <summary>
        /// 1A00..1A1F; Buginese
        /// </summary>
        public static ICodeSet Buginese {
            get {
                if (_buginese_ == null) {
                    _buginese_ = ((Code)0x1A00).Range (0x1A1F);
                }
                return _buginese_;
            }
        }
        private static ICodeSet _buginese_;

        /// <summary>
        /// 1B00..1B7F; Balinese
        /// </summary>
        public static ICodeSet Balinese {
            get {
                if (_balinese_ == null) {
                    _balinese_ = ((Code)0x1B00).Range (0x1B7F);
                }
                return _balinese_;
            }
        }
        private static ICodeSet _balinese_;

        /// <summary>
        /// 1D00..1D7F; Phonetic Extensions
        /// </summary>
        public static ICodeSet PhoneticExtensions {
            get {
                if (_phoneticExtensions_ == null) {
                    _phoneticExtensions_ = ((Code)0x1D00).Range (0x1D7F);
                }
                return _phoneticExtensions_;
            }
        }
        private static ICodeSet _phoneticExtensions_;

        /// <summary>
        /// 1D80..1DBF; Phonetic Extensions Supplement
        /// </summary>
        public static ICodeSet PhoneticExtensionsSupplement {
            get {
                if (_phoneticExtensionsSupplement_ == null) {
                    _phoneticExtensionsSupplement_ = ((Code)0x1D80).Range (0x1DBF);
                }
                return _phoneticExtensionsSupplement_;
            }
        }
        private static ICodeSet _phoneticExtensionsSupplement_;

        /// <summary>
        /// 1DC0..1DFF; Combining Diacritical Marks Supplement
        /// </summary>
        public static ICodeSet CombiningDiacriticalMarksSupplement {
            get {
                if (_combiningDiacriticalMarksSupplement_ == null) {
                    _combiningDiacriticalMarksSupplement_ = ((Code)0x1DC0).Range (0x1DFF);
                }
                return _combiningDiacriticalMarksSupplement_;
            }
        }
        private static ICodeSet _combiningDiacriticalMarksSupplement_;

        /// <summary>
        /// 1E00..1EFF; Latin Extended Additional
        /// </summary>
        public static ICodeSet LatinExtendedAdditional {
            get {
                if (_latinExtendedAdditional_ == null) {
                    _latinExtendedAdditional_ = ((Code)0x1E00).Range (0x1EFF);
                }
                return _latinExtendedAdditional_;
            }
        }
        private static ICodeSet _latinExtendedAdditional_;

        /// <summary>
        /// 1F00..1FFF; Greek Extended
        /// </summary>
        public static ICodeSet GreekExtended {
            get {
                if (_greekExtended_ == null) {
                    _greekExtended_ = ((Code)0x1F00).Range (0x1FFF);
                }
                return _greekExtended_;
            }
        }
        private static ICodeSet _greekExtended_;

        /// <summary>
        /// 2000..206F; General Punctuation
        /// </summary>
        public static ICodeSet GeneralPunctuation {
            get {
                if (_generalPunctuation_ == null) {
                    _generalPunctuation_ = ((Code)0x2000).Range (0x206F);
                }
                return _generalPunctuation_;
            }
        }
        private static ICodeSet _generalPunctuation_;

        /// <summary>
        /// 2070..209F; Superscripts and Subscripts
        /// </summary>
        public static ICodeSet SuperscriptsandSubscripts {
            get {
                if (_superscriptsandSubscripts_ == null) {
                    _superscriptsandSubscripts_ = ((Code)0x2070).Range (0x209F);
                }
                return _superscriptsandSubscripts_;
            }
        }
        private static ICodeSet _superscriptsandSubscripts_;

        /// <summary>
        /// 20A0..20CF; Currency Symbols
        /// </summary>
        public static ICodeSet CurrencySymbols {
            get {
                if (_currencySymbols_ == null) {
                    _currencySymbols_ = ((Code)0x20A0).Range (0x20CF);
                }
                return _currencySymbols_;
            }
        }
        private static ICodeSet _currencySymbols_;

        /// <summary>
        /// 20D0..20FF; Combining Diacritical Marks for Symbols
        /// </summary>
        public static ICodeSet CombiningDiacriticalMarksforSymbols {
            get {
                if (_combiningDiacriticalMarksforSymbols_ == null) {
                    _combiningDiacriticalMarksforSymbols_ = ((Code)0x20D0).Range (0x20FF);
                }
                return _combiningDiacriticalMarksforSymbols_;
            }
        }
        private static ICodeSet _combiningDiacriticalMarksforSymbols_;

        /// <summary>
        /// 2100..214F; Letterlike Symbols
        /// </summary>
        public static ICodeSet LetterlikeSymbols {
            get {
                if (_letterlikeSymbols_ == null) {
                    _letterlikeSymbols_ = ((Code)0x2100).Range (0x214F);
                }
                return _letterlikeSymbols_;
            }
        }
        private static ICodeSet _letterlikeSymbols_;

        /// <summary>
        /// 2150..218F; Number Forms
        /// </summary>
        public static ICodeSet NumberForms {
            get {
                if (_numberForms_ == null) {
                    _numberForms_ = ((Code)0x2150).Range (0x218F);
                }
                return _numberForms_;
            }
        }
        private static ICodeSet _numberForms_;

        /// <summary>
        /// 2190..21FF; Arrows
        /// </summary>
        public static ICodeSet Arrows {
            get {
                if (_arrows_ == null) {
                    _arrows_ = ((Code)0x2190).Range (0x21FF);
                }
                return _arrows_;
            }
        }
        private static ICodeSet _arrows_;

        /// <summary>
        /// 2200..22FF; Mathematical Operators
        /// </summary>
        public static ICodeSet MathematicalOperators {
            get {
                if (_mathematicalOperators_ == null) {
                    _mathematicalOperators_ = ((Code)0x2200).Range (0x22FF);
                }
                return _mathematicalOperators_;
            }
        }
        private static ICodeSet _mathematicalOperators_;

        /// <summary>
        /// 2300..23FF; Miscellaneous Technical
        /// </summary>
        public static ICodeSet MiscellaneousTechnical {
            get {
                if (_miscellaneousTechnical_ == null) {
                    _miscellaneousTechnical_ = ((Code)0x2300).Range (0x23FF);
                }
                return _miscellaneousTechnical_;
            }
        }
        private static ICodeSet _miscellaneousTechnical_;

        /// <summary>
        /// 2400..243F; Control Pictures
        /// </summary>
        public static ICodeSet ControlPictures {
            get {
                if (_controlPictures_ == null) {
                    _controlPictures_ = ((Code)0x2400).Range (0x243F);
                }
                return _controlPictures_;
            }
        }
        private static ICodeSet _controlPictures_;

        /// <summary>
        /// 2440..245F; Optical Character Recognition
        /// </summary>
        public static ICodeSet OpticalCharacterRecognition {
            get {
                if (_opticalCharacterRecognition_ == null) {
                    _opticalCharacterRecognition_ = ((Code)0x2440).Range (0x245F);
                }
                return _opticalCharacterRecognition_;
            }
        }
        private static ICodeSet _opticalCharacterRecognition_;

        /// <summary>
        /// 2460..24FF; Enclosed Alphanumerics
        /// </summary>
        public static ICodeSet EnclosedAlphanumerics {
            get {
                if (_enclosedAlphanumerics_ == null) {
                    _enclosedAlphanumerics_ = ((Code)0x2460).Range (0x24FF);
                }
                return _enclosedAlphanumerics_;
            }
        }
        private static ICodeSet _enclosedAlphanumerics_;

        /// <summary>
        /// 2500..257F; Box Drawing
        /// </summary>
        public static ICodeSet BoxDrawing {
            get {
                if (_boxDrawing_ == null) {
                    _boxDrawing_ = ((Code)0x2500).Range (0x257F);
                }
                return _boxDrawing_;
            }
        }
        private static ICodeSet _boxDrawing_;

        /// <summary>
        /// 2580..259F; Block Elements
        /// </summary>
        public static ICodeSet BlockElements {
            get {
                if (_blockElements_ == null) {
                    _blockElements_ = ((Code)0x2580).Range (0x259F);
                }
                return _blockElements_;
            }
        }
        private static ICodeSet _blockElements_;

        /// <summary>
        /// 25A0..25FF; Geometric Shapes
        /// </summary>
        public static ICodeSet GeometricShapes {
            get {
                if (_geometricShapes_ == null) {
                    _geometricShapes_ = ((Code)0x25A0).Range (0x25FF);
                }
                return _geometricShapes_;
            }
        }
        private static ICodeSet _geometricShapes_;

        /// <summary>
        /// 2600..26FF; Miscellaneous Symbols
        /// </summary>
        public static ICodeSet MiscellaneousSymbols {
            get {
                if (_miscellaneousSymbols_ == null) {
                    _miscellaneousSymbols_ = ((Code)0x2600).Range (0x26FF);
                }
                return _miscellaneousSymbols_;
            }
        }
        private static ICodeSet _miscellaneousSymbols_;

        /// <summary>
        /// 2700..27BF; Dingbats
        /// </summary>
        public static ICodeSet Dingbats {
            get {
                if (_dingbats_ == null) {
                    _dingbats_ = ((Code)0x2700).Range (0x27BF);
                }
                return _dingbats_;
            }
        }
        private static ICodeSet _dingbats_;

        /// <summary>
        /// 27C0..27EF; Miscellaneous Mathematical Symbols-A
        /// </summary>
        public static ICodeSet MiscellaneousMathematicalSymbolsA {
            get {
                if (_miscellaneousMathematicalSymbolsA_ == null) {
                    _miscellaneousMathematicalSymbolsA_ = ((Code)0x27C0).Range (0x27EF);
                }
                return _miscellaneousMathematicalSymbolsA_;
            }
        }
        private static ICodeSet _miscellaneousMathematicalSymbolsA_;

        /// <summary>
        /// 27F0..27FF; Supplemental Arrows-A
        /// </summary>
        public static ICodeSet SupplementalArrowsA {
            get {
                if (_supplementalArrowsA_ == null) {
                    _supplementalArrowsA_ = ((Code)0x27F0).Range (0x27FF);
                }
                return _supplementalArrowsA_;
            }
        }
        private static ICodeSet _supplementalArrowsA_;

        /// <summary>
        /// 2800..28FF; Braille Patterns
        /// </summary>
        public static ICodeSet BraillePatterns {
            get {
                if (_braillePatterns_ == null) {
                    _braillePatterns_ = ((Code)0x2800).Range (0x28FF);
                }
                return _braillePatterns_;
            }
        }
        private static ICodeSet _braillePatterns_;

        /// <summary>
        /// 2900..297F; Supplemental Arrows-B
        /// </summary>
        public static ICodeSet SupplementalArrowsB {
            get {
                if (_supplementalArrowsB_ == null) {
                    _supplementalArrowsB_ = ((Code)0x2900).Range (0x297F);
                }
                return _supplementalArrowsB_;
            }
        }
        private static ICodeSet _supplementalArrowsB_;

        /// <summary>
        /// 2980..29FF; Miscellaneous Mathematical Symbols-B
        /// </summary>
        public static ICodeSet MiscellaneousMathematicalSymbolsB {
            get {
                if (_miscellaneousMathematicalSymbolsB_ == null) {
                    _miscellaneousMathematicalSymbolsB_ = ((Code)0x2980).Range (0x29FF);
                }
                return _miscellaneousMathematicalSymbolsB_;
            }
        }
        private static ICodeSet _miscellaneousMathematicalSymbolsB_;

        /// <summary>
        /// 2A00..2AFF; Supplemental Mathematical Operators
        /// </summary>
        public static ICodeSet SupplementalMathematicalOperators {
            get {
                if (_supplementalMathematicalOperators_ == null) {
                    _supplementalMathematicalOperators_ = ((Code)0x2A00).Range (0x2AFF);
                }
                return _supplementalMathematicalOperators_;
            }
        }
        private static ICodeSet _supplementalMathematicalOperators_;

        /// <summary>
        /// 2B00..2BFF; Miscellaneous Symbols and Arrows
        /// </summary>
        public static ICodeSet MiscellaneousSymbolsandArrows {
            get {
                if (_miscellaneousSymbolsandArrows_ == null) {
                    _miscellaneousSymbolsandArrows_ = ((Code)0x2B00).Range (0x2BFF);
                }
                return _miscellaneousSymbolsandArrows_;
            }
        }
        private static ICodeSet _miscellaneousSymbolsandArrows_;

        /// <summary>
        /// 2C00..2C5F; Glagolitic
        /// </summary>
        public static ICodeSet Glagolitic {
            get {
                if (_glagolitic_ == null) {
                    _glagolitic_ = ((Code)0x2C00).Range (0x2C5F);
                }
                return _glagolitic_;
            }
        }
        private static ICodeSet _glagolitic_;

        /// <summary>
        /// 2C60..2C7F; Latin Extended-C
        /// </summary>
        public static ICodeSet LatinExtendedC {
            get {
                if (_latinExtendedC_ == null) {
                    _latinExtendedC_ = ((Code)0x2C60).Range (0x2C7F);
                }
                return _latinExtendedC_;
            }
        }
        private static ICodeSet _latinExtendedC_;

        /// <summary>
        /// 2C80..2CFF; Coptic
        /// </summary>
        public static ICodeSet Coptic {
            get {
                if (_coptic_ == null) {
                    _coptic_ = ((Code)0x2C80).Range (0x2CFF);
                }
                return _coptic_;
            }
        }
        private static ICodeSet _coptic_;

        /// <summary>
        /// 2D00..2D2F; Georgian Supplement
        /// </summary>
        public static ICodeSet GeorgianSupplement {
            get {
                if (_georgianSupplement_ == null) {
                    _georgianSupplement_ = ((Code)0x2D00).Range (0x2D2F);
                }
                return _georgianSupplement_;
            }
        }
        private static ICodeSet _georgianSupplement_;

        /// <summary>
        /// 2D30..2D7F; Tifinagh
        /// </summary>
        public static ICodeSet Tifinagh {
            get {
                if (_tifinagh_ == null) {
                    _tifinagh_ = ((Code)0x2D30).Range (0x2D7F);
                }
                return _tifinagh_;
            }
        }
        private static ICodeSet _tifinagh_;

        /// <summary>
        /// 2D80..2DDF; Ethiopic Extended
        /// </summary>
        public static ICodeSet EthiopicExtended {
            get {
                if (_ethiopicExtended_ == null) {
                    _ethiopicExtended_ = ((Code)0x2D80).Range (0x2DDF);
                }
                return _ethiopicExtended_;
            }
        }
        private static ICodeSet _ethiopicExtended_;

        /// <summary>
        /// 2E00..2E7F; Supplemental Punctuation
        /// </summary>
        public static ICodeSet SupplementalPunctuation {
            get {
                if (_supplementalPunctuation_ == null) {
                    _supplementalPunctuation_ = ((Code)0x2E00).Range (0x2E7F);
                }
                return _supplementalPunctuation_;
            }
        }
        private static ICodeSet _supplementalPunctuation_;

        /// <summary>
        /// 2E80..2EFF; CJK Radicals Supplement
        /// </summary>
        public static ICodeSet CJKRadicalsSupplement {
            get {
                if (_cJKRadicalsSupplement_ == null) {
                    _cJKRadicalsSupplement_ = ((Code)0x2E80).Range (0x2EFF);
                }
                return _cJKRadicalsSupplement_;
            }
        }
        private static ICodeSet _cJKRadicalsSupplement_;

        /// <summary>
        /// 2F00..2FDF; Kangxi Radicals
        /// </summary>
        public static ICodeSet KangxiRadicals {
            get {
                if (_kangxiRadicals_ == null) {
                    _kangxiRadicals_ = ((Code)0x2F00).Range (0x2FDF);
                }
                return _kangxiRadicals_;
            }
        }
        private static ICodeSet _kangxiRadicals_;

        /// <summary>
        /// 2FF0..2FFF; Ideographic Description Characters
        /// </summary>
        public static ICodeSet IdeographicDescriptionCharacters {
            get {
                if (_ideographicDescriptionCharacters_ == null) {
                    _ideographicDescriptionCharacters_ = ((Code)0x2FF0).Range (0x2FFF);
                }
                return _ideographicDescriptionCharacters_;
            }
        }
        private static ICodeSet _ideographicDescriptionCharacters_;

        /// <summary>
        /// 3000..303F; CJK Symbols and Punctuation
        /// </summary>
        public static ICodeSet CJKSymbolsandPunctuation {
            get {
                if (_cJKSymbolsandPunctuation_ == null) {
                    _cJKSymbolsandPunctuation_ = ((Code)0x3000).Range (0x303F);
                }
                return _cJKSymbolsandPunctuation_;
            }
        }
        private static ICodeSet _cJKSymbolsandPunctuation_;

        /// <summary>
        /// 3040..309F; Hiragana
        /// </summary>
        public static ICodeSet Hiragana {
            get {
                if (_hiragana_ == null) {
                    _hiragana_ = ((Code)0x3040).Range (0x309F);
                }
                return _hiragana_;
            }
        }
        private static ICodeSet _hiragana_;

        /// <summary>
        /// 30A0..30FF; Katakana
        /// </summary>
        public static ICodeSet Katakana {
            get {
                if (_katakana_ == null) {
                    _katakana_ = ((Code)0x30A0).Range (0x30FF);
                }
                return _katakana_;
            }
        }
        private static ICodeSet _katakana_;

        /// <summary>
        /// 3100..312F; Bopomofo
        /// </summary>
        public static ICodeSet Bopomofo {
            get {
                if (_bopomofo_ == null) {
                    _bopomofo_ = ((Code)0x3100).Range (0x312F);
                }
                return _bopomofo_;
            }
        }
        private static ICodeSet _bopomofo_;

        /// <summary>
        /// 3130..318F; Hangul Compatibility Jamo
        /// </summary>
        public static ICodeSet HangulCompatibilityJamo {
            get {
                if (_hangulCompatibilityJamo_ == null) {
                    _hangulCompatibilityJamo_ = ((Code)0x3130).Range (0x318F);
                }
                return _hangulCompatibilityJamo_;
            }
        }
        private static ICodeSet _hangulCompatibilityJamo_;

        /// <summary>
        /// 3190..319F; Kanbun
        /// </summary>
        public static ICodeSet Kanbun {
            get {
                if (_kanbun_ == null) {
                    _kanbun_ = ((Code)0x3190).Range (0x319F);
                }
                return _kanbun_;
            }
        }
        private static ICodeSet _kanbun_;

        /// <summary>
        /// 31A0..31BF; Bopomofo Extended
        /// </summary>
        public static ICodeSet BopomofoExtended {
            get {
                if (_bopomofoExtended_ == null) {
                    _bopomofoExtended_ = ((Code)0x31A0).Range (0x31BF);
                }
                return _bopomofoExtended_;
            }
        }
        private static ICodeSet _bopomofoExtended_;

        /// <summary>
        /// 31C0..31EF; CJK Strokes
        /// </summary>
        public static ICodeSet CJKStrokes {
            get {
                if (_cJKStrokes_ == null) {
                    _cJKStrokes_ = ((Code)0x31C0).Range (0x31EF);
                }
                return _cJKStrokes_;
            }
        }
        private static ICodeSet _cJKStrokes_;

        /// <summary>
        /// 31F0..31FF; Katakana Phonetic Extensions
        /// </summary>
        public static ICodeSet KatakanaPhoneticExtensions {
            get {
                if (_katakanaPhoneticExtensions_ == null) {
                    _katakanaPhoneticExtensions_ = ((Code)0x31F0).Range (0x31FF);
                }
                return _katakanaPhoneticExtensions_;
            }
        }
        private static ICodeSet _katakanaPhoneticExtensions_;

        /// <summary>
        /// 3200..32FF; Enclosed CJK Letters and Months
        /// </summary>
        public static ICodeSet EnclosedCJKLettersandMonths {
            get {
                if (_enclosedCJKLettersandMonths_ == null) {
                    _enclosedCJKLettersandMonths_ = ((Code)0x3200).Range (0x32FF);
                }
                return _enclosedCJKLettersandMonths_;
            }
        }
        private static ICodeSet _enclosedCJKLettersandMonths_;

        /// <summary>
        /// 3300..33FF; CJK Compatibility
        /// </summary>
        public static ICodeSet CJKCompatibility {
            get {
                if (_cJKCompatibility_ == null) {
                    _cJKCompatibility_ = ((Code)0x3300).Range (0x33FF);
                }
                return _cJKCompatibility_;
            }
        }
        private static ICodeSet _cJKCompatibility_;

        /// <summary>
        /// 3400..4DBF; CJK Unified Ideographs Extension A
        /// </summary>
        public static ICodeSet CJKUnifiedIdeographsExtensionA {
            get {
                if (_cJKUnifiedIdeographsExtensionA_ == null) {
                    _cJKUnifiedIdeographsExtensionA_ = ((Code)0x3400).Range (0x4DBF);
                }
                return _cJKUnifiedIdeographsExtensionA_;
            }
        }
        private static ICodeSet _cJKUnifiedIdeographsExtensionA_;

        /// <summary>
        /// 4DC0..4DFF; Yijing Hexagram Symbols
        /// </summary>
        public static ICodeSet YijingHexagramSymbols {
            get {
                if (_yijingHexagramSymbols_ == null) {
                    _yijingHexagramSymbols_ = ((Code)0x4DC0).Range (0x4DFF);
                }
                return _yijingHexagramSymbols_;
            }
        }
        private static ICodeSet _yijingHexagramSymbols_;

        /// <summary>
        /// 4E00..9FFF; CJK Unified Ideographs
        /// </summary>
        public static ICodeSet CJKUnifiedIdeographs {
            get {
                if (_cJKUnifiedIdeographs_ == null) {
                    _cJKUnifiedIdeographs_ = ((Code)0x4E00).Range (0x9FFF);
                }
                return _cJKUnifiedIdeographs_;
            }
        }
        private static ICodeSet _cJKUnifiedIdeographs_;

        /// <summary>
        /// A000..A48F; Yi Syllables
        /// </summary>
        public static ICodeSet YiSyllables {
            get {
                if (_yiSyllables_ == null) {
                    _yiSyllables_ = ((Code)0xA000).Range (0xA48F);
                }
                return _yiSyllables_;
            }
        }
        private static ICodeSet _yiSyllables_;

        /// <summary>
        /// A490..A4CF; Yi Radicals
        /// </summary>
        public static ICodeSet YiRadicals {
            get {
                if (_yiRadicals_ == null) {
                    _yiRadicals_ = ((Code)0xA490).Range (0xA4CF);
                }
                return _yiRadicals_;
            }
        }
        private static ICodeSet _yiRadicals_;

        /// <summary>
        /// A700..A71F; Modifier Tone Letters
        /// </summary>
        public static ICodeSet ModifierToneLetters {
            get {
                if (_modifierToneLetters_ == null) {
                    _modifierToneLetters_ = ((Code)0xA700).Range (0xA71F);
                }
                return _modifierToneLetters_;
            }
        }
        private static ICodeSet _modifierToneLetters_;

        /// <summary>
        /// A720..A7FF; Latin Extended-D
        /// </summary>
        public static ICodeSet LatinExtendedD {
            get {
                if (_latinExtendedD_ == null) {
                    _latinExtendedD_ = ((Code)0xA720).Range (0xA7FF);
                }
                return _latinExtendedD_;
            }
        }
        private static ICodeSet _latinExtendedD_;

        /// <summary>
        /// A800..A82F; Syloti Nagri
        /// </summary>
        public static ICodeSet SylotiNagri {
            get {
                if (_sylotiNagri_ == null) {
                    _sylotiNagri_ = ((Code)0xA800).Range (0xA82F);
                }
                return _sylotiNagri_;
            }
        }
        private static ICodeSet _sylotiNagri_;

        /// <summary>
        /// A840..A87F; Phags-pa
        /// </summary>
        public static ICodeSet Phagspa {
            get {
                if (_phagspa_ == null) {
                    _phagspa_ = ((Code)0xA840).Range (0xA87F);
                }
                return _phagspa_;
            }
        }
        private static ICodeSet _phagspa_;

        /// <summary>
        /// AC00..D7AF; Hangul Syllables
        /// </summary>
        public static ICodeSet HangulSyllables {
            get {
                if (_hangulSyllables_ == null) {
                    _hangulSyllables_ = ((Code)0xAC00).Range (0xD7AF);
                }
                return _hangulSyllables_;
            }
        }
        private static ICodeSet _hangulSyllables_;

        /// <summary>
        /// D800..DB7F; High Surrogates
        /// </summary>
        public static ICodeSet HighSurrogates {
            get {
                if (_highSurrogates_ == null) {
                    _highSurrogates_ = ((Code)0xD800).Range (0xDB7F);
                }
                return _highSurrogates_;
            }
        }
        private static ICodeSet _highSurrogates_;

        /// <summary>
        /// DB80..DBFF; High Private Use Surrogates
        /// </summary>
        public static ICodeSet HighPrivateUseSurrogates {
            get {
                if (_highPrivateUseSurrogates_ == null) {
                    _highPrivateUseSurrogates_ = ((Code)0xDB80).Range (0xDBFF);
                }
                return _highPrivateUseSurrogates_;
            }
        }
        private static ICodeSet _highPrivateUseSurrogates_;

        /// <summary>
        /// DC00..DFFF; Low Surrogates
        /// </summary>
        public static ICodeSet LowSurrogates {
            get {
                if (_lowSurrogates_ == null) {
                    _lowSurrogates_ = ((Code)0xDC00).Range (0xDFFF);
                }
                return _lowSurrogates_;
            }
        }
        private static ICodeSet _lowSurrogates_;

        /// <summary>
        /// E000..F8FF; Private Use Area
        /// </summary>
        public static ICodeSet PrivateUseArea {
            get {
                if (_privateUseArea_ == null) {
                    _privateUseArea_ = ((Code)0xE000).Range (0xF8FF);
                }
                return _privateUseArea_;
            }
        }
        private static ICodeSet _privateUseArea_;

        /// <summary>
        /// F900..FAFF; CJK Compatibility Ideographs
        /// </summary>
        public static ICodeSet CJKCompatibilityIdeographs {
            get {
                if (_cJKCompatibilityIdeographs_ == null) {
                    _cJKCompatibilityIdeographs_ = ((Code)0xF900).Range (0xFAFF);
                }
                return _cJKCompatibilityIdeographs_;
            }
        }
        private static ICodeSet _cJKCompatibilityIdeographs_;

        /// <summary>
        /// FB00..FB4F; Alphabetic Presentation Forms
        /// </summary>
        public static ICodeSet AlphabeticPresentationForms {
            get {
                if (_alphabeticPresentationForms_ == null) {
                    _alphabeticPresentationForms_ = ((Code)0xFB00).Range (0xFB4F);
                }
                return _alphabeticPresentationForms_;
            }
        }
        private static ICodeSet _alphabeticPresentationForms_;

        /// <summary>
        /// FB50..FDFF; Arabic Presentation Forms-A
        /// </summary>
        public static ICodeSet ArabicPresentationFormsA {
            get {
                if (_arabicPresentationFormsA_ == null) {
                    _arabicPresentationFormsA_ = ((Code)0xFB50).Range (0xFDFF);
                }
                return _arabicPresentationFormsA_;
            }
        }
        private static ICodeSet _arabicPresentationFormsA_;

        /// <summary>
        /// FE00..FE0F; Variation Selectors
        /// </summary>
        public static ICodeSet VariationSelectors {
            get {
                if (_variationSelectors_ == null) {
                    _variationSelectors_ = ((Code)0xFE00).Range (0xFE0F);
                }
                return _variationSelectors_;
            }
        }
        private static ICodeSet _variationSelectors_;

        /// <summary>
        /// FE10..FE1F; Vertical Forms
        /// </summary>
        public static ICodeSet VerticalForms {
            get {
                if (_verticalForms_ == null) {
                    _verticalForms_ = ((Code)0xFE10).Range (0xFE1F);
                }
                return _verticalForms_;
            }
        }
        private static ICodeSet _verticalForms_;

        /// <summary>
        /// FE20..FE2F; Combining Half Marks
        /// </summary>
        public static ICodeSet CombiningHalfMarks {
            get {
                if (_combiningHalfMarks_ == null) {
                    _combiningHalfMarks_ = ((Code)0xFE20).Range (0xFE2F);
                }
                return _combiningHalfMarks_;
            }
        }
        private static ICodeSet _combiningHalfMarks_;

        /// <summary>
        /// FE30..FE4F; CJK Compatibility Forms
        /// </summary>
        public static ICodeSet CJKCompatibilityForms {
            get {
                if (_cJKCompatibilityForms_ == null) {
                    _cJKCompatibilityForms_ = ((Code)0xFE30).Range (0xFE4F);
                }
                return _cJKCompatibilityForms_;
            }
        }
        private static ICodeSet _cJKCompatibilityForms_;

        /// <summary>
        /// FE50..FE6F; Small Form Variants
        /// </summary>
        public static ICodeSet SmallFormVariants {
            get {
                if (_smallFormVariants_ == null) {
                    _smallFormVariants_ = ((Code)0xFE50).Range (0xFE6F);
                }
                return _smallFormVariants_;
            }
        }
        private static ICodeSet _smallFormVariants_;

        /// <summary>
        /// FE70..FEFF; Arabic Presentation Forms-B
        /// </summary>
        public static ICodeSet ArabicPresentationFormsB {
            get {
                if (_arabicPresentationFormsB_ == null) {
                    _arabicPresentationFormsB_ = ((Code)0xFE70).Range (0xFEFF);
                }
                return _arabicPresentationFormsB_;
            }
        }
        private static ICodeSet _arabicPresentationFormsB_;

        /// <summary>
        /// FF00..FFEF; Halfwidth and Fullwidth Forms
        /// </summary>
        public static ICodeSet HalfwidthandFullwidthForms {
            get {
                if (_halfwidthandFullwidthForms_ == null) {
                    _halfwidthandFullwidthForms_ = ((Code)0xFF00).Range (0xFFEF);
                }
                return _halfwidthandFullwidthForms_;
            }
        }
        private static ICodeSet _halfwidthandFullwidthForms_;

        /// <summary>
        /// FFF0..FFFF; Specials
        /// </summary>
        public static ICodeSet Specials {
            get {
                if (_specials_ == null) {
                    _specials_ = ((Code)0xFFF0).Range (0xFFFF);
                }
                return _specials_;
            }
        }
        private static ICodeSet _specials_;

        /// <summary>
        /// 10000..1007F; Linear B Syllabary
        /// </summary>
        public static ICodeSet LinearBSyllabary {
            get {
                if (_linearBSyllabary_ == null) {
                    _linearBSyllabary_ = ((Code)0x10000).Range (0x1007F);
                }
                return _linearBSyllabary_;
            }
        }
        private static ICodeSet _linearBSyllabary_;

        /// <summary>
        /// 10080..100FF; Linear B Ideograms
        /// </summary>
        public static ICodeSet LinearBIdeograms {
            get {
                if (_linearBIdeograms_ == null) {
                    _linearBIdeograms_ = ((Code)0x10080).Range (0x100FF);
                }
                return _linearBIdeograms_;
            }
        }
        private static ICodeSet _linearBIdeograms_;

        /// <summary>
        /// 10100..1013F; Aegean Numbers
        /// </summary>
        public static ICodeSet AegeanNumbers {
            get {
                if (_aegeanNumbers_ == null) {
                    _aegeanNumbers_ = ((Code)0x10100).Range (0x1013F);
                }
                return _aegeanNumbers_;
            }
        }
        private static ICodeSet _aegeanNumbers_;

        /// <summary>
        /// 10140..1018F; Ancient Greek Numbers
        /// </summary>
        public static ICodeSet AncientGreekNumbers {
            get {
                if (_ancientGreekNumbers_ == null) {
                    _ancientGreekNumbers_ = ((Code)0x10140).Range (0x1018F);
                }
                return _ancientGreekNumbers_;
            }
        }
        private static ICodeSet _ancientGreekNumbers_;

        /// <summary>
        /// 10300..1032F; Old Italic
        /// </summary>
        public static ICodeSet OldItalic {
            get {
                if (_oldItalic_ == null) {
                    _oldItalic_ = ((Code)0x10300).Range (0x1032F);
                }
                return _oldItalic_;
            }
        }
        private static ICodeSet _oldItalic_;

        /// <summary>
        /// 10330..1034F; Gothic
        /// </summary>
        public static ICodeSet Gothic {
            get {
                if (_gothic_ == null) {
                    _gothic_ = ((Code)0x10330).Range (0x1034F);
                }
                return _gothic_;
            }
        }
        private static ICodeSet _gothic_;

        /// <summary>
        /// 10380..1039F; Ugaritic
        /// </summary>
        public static ICodeSet Ugaritic {
            get {
                if (_ugaritic_ == null) {
                    _ugaritic_ = ((Code)0x10380).Range (0x1039F);
                }
                return _ugaritic_;
            }
        }
        private static ICodeSet _ugaritic_;

        /// <summary>
        /// 103A0..103DF; Old Persian
        /// </summary>
        public static ICodeSet OldPersian {
            get {
                if (_oldPersian_ == null) {
                    _oldPersian_ = ((Code)0x103A0).Range (0x103DF);
                }
                return _oldPersian_;
            }
        }
        private static ICodeSet _oldPersian_;

        /// <summary>
        /// 10400..1044F; Deseret
        /// </summary>
        public static ICodeSet Deseret {
            get {
                if (_deseret_ == null) {
                    _deseret_ = ((Code)0x10400).Range (0x1044F);
                }
                return _deseret_;
            }
        }
        private static ICodeSet _deseret_;

        /// <summary>
        /// 10450..1047F; Shavian
        /// </summary>
        public static ICodeSet Shavian {
            get {
                if (_shavian_ == null) {
                    _shavian_ = ((Code)0x10450).Range (0x1047F);
                }
                return _shavian_;
            }
        }
        private static ICodeSet _shavian_;

        /// <summary>
        /// 10480..104AF; Osmanya
        /// </summary>
        public static ICodeSet Osmanya {
            get {
                if (_osmanya_ == null) {
                    _osmanya_ = ((Code)0x10480).Range (0x104AF);
                }
                return _osmanya_;
            }
        }
        private static ICodeSet _osmanya_;

        /// <summary>
        /// 10800..1083F; Cypriot Syllabary
        /// </summary>
        public static ICodeSet CypriotSyllabary {
            get {
                if (_cypriotSyllabary_ == null) {
                    _cypriotSyllabary_ = ((Code)0x10800).Range (0x1083F);
                }
                return _cypriotSyllabary_;
            }
        }
        private static ICodeSet _cypriotSyllabary_;

        /// <summary>
        /// 10900..1091F; Phoenician
        /// </summary>
        public static ICodeSet Phoenician {
            get {
                if (_phoenician_ == null) {
                    _phoenician_ = ((Code)0x10900).Range (0x1091F);
                }
                return _phoenician_;
            }
        }
        private static ICodeSet _phoenician_;

        /// <summary>
        /// 10A00..10A5F; Kharoshthi
        /// </summary>
        public static ICodeSet Kharoshthi {
            get {
                if (_kharoshthi_ == null) {
                    _kharoshthi_ = ((Code)0x10A00).Range (0x10A5F);
                }
                return _kharoshthi_;
            }
        }
        private static ICodeSet _kharoshthi_;

        /// <summary>
        /// 12000..123FF; Cuneiform
        /// </summary>
        public static ICodeSet Cuneiform {
            get {
                if (_cuneiform_ == null) {
                    _cuneiform_ = ((Code)0x12000).Range (0x123FF);
                }
                return _cuneiform_;
            }
        }
        private static ICodeSet _cuneiform_;

        /// <summary>
        /// 12400..1247F; Cuneiform Numbers and Punctuation
        /// </summary>
        public static ICodeSet CuneiformNumbersandPunctuation {
            get {
                if (_cuneiformNumbersandPunctuation_ == null) {
                    _cuneiformNumbersandPunctuation_ = ((Code)0x12400).Range (0x1247F);
                }
                return _cuneiformNumbersandPunctuation_;
            }
        }
        private static ICodeSet _cuneiformNumbersandPunctuation_;

        /// <summary>
        /// 1D000..1D0FF; Byzantine Musical Symbols
        /// </summary>
        public static ICodeSet ByzantineMusicalSymbols {
            get {
                if (_byzantineMusicalSymbols_ == null) {
                    _byzantineMusicalSymbols_ = ((Code)0x1D000).Range (0x1D0FF);
                }
                return _byzantineMusicalSymbols_;
            }
        }
        private static ICodeSet _byzantineMusicalSymbols_;

        /// <summary>
        /// 1D100..1D1FF; Musical Symbols
        /// </summary>
        public static ICodeSet MusicalSymbols {
            get {
                if (_musicalSymbols_ == null) {
                    _musicalSymbols_ = ((Code)0x1D100).Range (0x1D1FF);
                }
                return _musicalSymbols_;
            }
        }
        private static ICodeSet _musicalSymbols_;

        /// <summary>
        /// 1D200..1D24F; Ancient Greek Musical Notation
        /// </summary>
        public static ICodeSet AncientGreekMusicalNotation {
            get {
                if (_ancientGreekMusicalNotation_ == null) {
                    _ancientGreekMusicalNotation_ = ((Code)0x1D200).Range (0x1D24F);
                }
                return _ancientGreekMusicalNotation_;
            }
        }
        private static ICodeSet _ancientGreekMusicalNotation_;

        /// <summary>
        /// 1D300..1D35F; Tai Xuan Jing Symbols
        /// </summary>
        public static ICodeSet TaiXuanJingSymbols {
            get {
                if (_taiXuanJingSymbols_ == null) {
                    _taiXuanJingSymbols_ = ((Code)0x1D300).Range (0x1D35F);
                }
                return _taiXuanJingSymbols_;
            }
        }
        private static ICodeSet _taiXuanJingSymbols_;

        /// <summary>
        /// 1D360..1D37F; Counting Rod Numerals
        /// </summary>
        public static ICodeSet CountingRodNumerals {
            get {
                if (_countingRodNumerals_ == null) {
                    _countingRodNumerals_ = ((Code)0x1D360).Range (0x1D37F);
                }
                return _countingRodNumerals_;
            }
        }
        private static ICodeSet _countingRodNumerals_;

        /// <summary>
        /// 1D400..1D7FF; Mathematical Alphanumeric Symbols
        /// </summary>
        public static ICodeSet MathematicalAlphanumericSymbols {
            get {
                if (_mathematicalAlphanumericSymbols_ == null) {
                    _mathematicalAlphanumericSymbols_ = ((Code)0x1D400).Range (0x1D7FF);
                }
                return _mathematicalAlphanumericSymbols_;
            }
        }
        private static ICodeSet _mathematicalAlphanumericSymbols_;

        /// <summary>
        /// 20000..2A6DF; CJK Unified Ideographs Extension B
        /// </summary>
        public static ICodeSet CJKUnifiedIdeographsExtensionB {
            get {
                if (_cJKUnifiedIdeographsExtensionB_ == null) {
                    _cJKUnifiedIdeographsExtensionB_ = ((Code)0x20000).Range (0x2A6DF);
                }
                return _cJKUnifiedIdeographsExtensionB_;
            }
        }
        private static ICodeSet _cJKUnifiedIdeographsExtensionB_;

        /// <summary>
        /// 2F800..2FA1F; CJK Compatibility Ideographs Supplement
        /// </summary>
        public static ICodeSet CJKCompatibilityIdeographsSupplement {
            get {
                if (_cJKCompatibilityIdeographsSupplement_ == null) {
                    _cJKCompatibilityIdeographsSupplement_ = ((Code)0x2F800).Range (0x2FA1F);
                }
                return _cJKCompatibilityIdeographsSupplement_;
            }
        }
        private static ICodeSet _cJKCompatibilityIdeographsSupplement_;

        /// <summary>
        /// E0000..E007F; Tags
        /// </summary>
        public static ICodeSet Tags {
            get {
                if (_tags_ == null) {
                    _tags_ = ((Code)0xE0000).Range (0xE007F);
                }
                return _tags_;
            }
        }
        private static ICodeSet _tags_;

        /// <summary>
        /// E0100..E01EF; Variation Selectors Supplement
        /// </summary>
        public static ICodeSet VariationSelectorsSupplement {
            get {
                if (_variationSelectorsSupplement_ == null) {
                    _variationSelectorsSupplement_ = ((Code)0xE0100).Range (0xE01EF);
                }
                return _variationSelectorsSupplement_;
            }
        }
        private static ICodeSet _variationSelectorsSupplement_;

        /// <summary>
        /// F0000..FFFFF; Supplementary Private Use Area-A
        /// </summary>
        public static ICodeSet SupplementaryPrivateUseAreaA {
            get {
                if (_supplementaryPrivateUseAreaA_ == null) {
                    _supplementaryPrivateUseAreaA_ = ((Code)0xF0000).Range (0xFFFFF);
                }
                return _supplementaryPrivateUseAreaA_;
            }
        }
        private static ICodeSet _supplementaryPrivateUseAreaA_;

        /// <summary>
        /// 100000..10FFFF; Supplementary Private Use Area-B
        /// </summary>
        public static ICodeSet SupplementaryPrivateUseAreaB {
            get {
                if (_supplementaryPrivateUseAreaB_ == null) {
                    _supplementaryPrivateUseAreaB_ = ((Code)0x100000).Range (0x10FFFF);
                }
                return _supplementaryPrivateUseAreaB_;
            }
        }
        private static ICodeSet _supplementaryPrivateUseAreaB_;


        #endregion
    }
}
