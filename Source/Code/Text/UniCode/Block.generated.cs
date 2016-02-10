// ================================================================================
// <copyright file="https://github.com/ddur/DBCL/blob/master/LICENSE" company="DD">
// Copyright © 2013-2016 Dragan Duric. All Rights Reserved.
// ================================================================================
// GENERATED CLASS - DO NOT CHANGE
// ================================================================================

using System;
using DD.Collections.ICodeSet;

namespace DD.Text.UniCode {

    public static partial class Block {

        /// <summary>
        /// 0000..007F; Basic Latin
        /// </summary>
        public static ICodeSet BasicLatin {
            get {
                if (_basicLatin_ == null) {
                    _basicLatin_ = CodeSetFull.From (0x0000, 0x007F);
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
                    _latin1Supplement_ = CodeSetFull.From (0x0080, 0x00FF);
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
                    _latinExtendedA_ = CodeSetFull.From (0x0100, 0x017F);
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
                    _latinExtendedB_ = CodeSetFull.From (0x0180, 0x024F);
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
                    _iPAExtensions_ = CodeSetFull.From (0x0250, 0x02AF);
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
                    _spacingModifierLetters_ = CodeSetFull.From (0x02B0, 0x02FF);
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
                    _combiningDiacriticalMarks_ = CodeSetFull.From (0x0300, 0x036F);
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
                    _greekandCoptic_ = CodeSetFull.From (0x0370, 0x03FF);
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
                    _cyrillic_ = CodeSetFull.From (0x0400, 0x04FF);
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
                    _cyrillicSupplement_ = CodeSetFull.From (0x0500, 0x052F);
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
                    _armenian_ = CodeSetFull.From (0x0530, 0x058F);
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
                    _hebrew_ = CodeSetFull.From (0x0590, 0x05FF);
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
                    _arabic_ = CodeSetFull.From (0x0600, 0x06FF);
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
                    _syriac_ = CodeSetFull.From (0x0700, 0x074F);
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
                    _arabicSupplement_ = CodeSetFull.From (0x0750, 0x077F);
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
                    _thaana_ = CodeSetFull.From (0x0780, 0x07BF);
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
                    _nKo_ = CodeSetFull.From (0x07C0, 0x07FF);
                }
                return _nKo_;
            }
        }
        private static ICodeSet _nKo_;

        /// <summary>
        /// 0800..083F; Samaritan
        /// </summary>
        public static ICodeSet Samaritan {
            get {
                if (_samaritan_ == null) {
                    _samaritan_ = CodeSetFull.From (0x0800, 0x083F);
                }
                return _samaritan_;
            }
        }
        private static ICodeSet _samaritan_;

        /// <summary>
        /// 0840..085F; Mandaic
        /// </summary>
        public static ICodeSet Mandaic {
            get {
                if (_mandaic_ == null) {
                    _mandaic_ = CodeSetFull.From (0x0840, 0x085F);
                }
                return _mandaic_;
            }
        }
        private static ICodeSet _mandaic_;

        /// <summary>
        /// 08A0..08FF; Arabic Extended-A
        /// </summary>
        public static ICodeSet ArabicExtendedA {
            get {
                if (_arabicExtendedA_ == null) {
                    _arabicExtendedA_ = CodeSetFull.From (0x08A0, 0x08FF);
                }
                return _arabicExtendedA_;
            }
        }
        private static ICodeSet _arabicExtendedA_;

        /// <summary>
        /// 0900..097F; Devanagari
        /// </summary>
        public static ICodeSet Devanagari {
            get {
                if (_devanagari_ == null) {
                    _devanagari_ = CodeSetFull.From (0x0900, 0x097F);
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
                    _bengali_ = CodeSetFull.From (0x0980, 0x09FF);
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
                    _gurmukhi_ = CodeSetFull.From (0x0A00, 0x0A7F);
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
                    _gujarati_ = CodeSetFull.From (0x0A80, 0x0AFF);
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
                    _oriya_ = CodeSetFull.From (0x0B00, 0x0B7F);
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
                    _tamil_ = CodeSetFull.From (0x0B80, 0x0BFF);
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
                    _telugu_ = CodeSetFull.From (0x0C00, 0x0C7F);
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
                    _kannada_ = CodeSetFull.From (0x0C80, 0x0CFF);
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
                    _malayalam_ = CodeSetFull.From (0x0D00, 0x0D7F);
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
                    _sinhala_ = CodeSetFull.From (0x0D80, 0x0DFF);
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
                    _thai_ = CodeSetFull.From (0x0E00, 0x0E7F);
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
                    _lao_ = CodeSetFull.From (0x0E80, 0x0EFF);
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
                    _tibetan_ = CodeSetFull.From (0x0F00, 0x0FFF);
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
                    _myanmar_ = CodeSetFull.From (0x1000, 0x109F);
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
                    _georgian_ = CodeSetFull.From (0x10A0, 0x10FF);
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
                    _hangulJamo_ = CodeSetFull.From (0x1100, 0x11FF);
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
                    _ethiopic_ = CodeSetFull.From (0x1200, 0x137F);
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
                    _ethiopicSupplement_ = CodeSetFull.From (0x1380, 0x139F);
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
                    _cherokee_ = CodeSetFull.From (0x13A0, 0x13FF);
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
                    _unifiedCanadianAboriginalSyllabics_ = CodeSetFull.From (0x1400, 0x167F);
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
                    _ogham_ = CodeSetFull.From (0x1680, 0x169F);
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
                    _runic_ = CodeSetFull.From (0x16A0, 0x16FF);
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
                    _tagalog_ = CodeSetFull.From (0x1700, 0x171F);
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
                    _hanunoo_ = CodeSetFull.From (0x1720, 0x173F);
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
                    _buhid_ = CodeSetFull.From (0x1740, 0x175F);
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
                    _tagbanwa_ = CodeSetFull.From (0x1760, 0x177F);
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
                    _khmer_ = CodeSetFull.From (0x1780, 0x17FF);
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
                    _mongolian_ = CodeSetFull.From (0x1800, 0x18AF);
                }
                return _mongolian_;
            }
        }
        private static ICodeSet _mongolian_;

        /// <summary>
        /// 18B0..18FF; Unified Canadian Aboriginal Syllabics Extended
        /// </summary>
        public static ICodeSet UnifiedCanadianAboriginalSyllabicsExtended {
            get {
                if (_unifiedCanadianAboriginalSyllabicsExtended_ == null) {
                    _unifiedCanadianAboriginalSyllabicsExtended_ = CodeSetFull.From (0x18B0, 0x18FF);
                }
                return _unifiedCanadianAboriginalSyllabicsExtended_;
            }
        }
        private static ICodeSet _unifiedCanadianAboriginalSyllabicsExtended_;

        /// <summary>
        /// 1900..194F; Limbu
        /// </summary>
        public static ICodeSet Limbu {
            get {
                if (_limbu_ == null) {
                    _limbu_ = CodeSetFull.From (0x1900, 0x194F);
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
                    _taiLe_ = CodeSetFull.From (0x1950, 0x197F);
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
                    _newTaiLue_ = CodeSetFull.From (0x1980, 0x19DF);
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
                    _khmerSymbols_ = CodeSetFull.From (0x19E0, 0x19FF);
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
                    _buginese_ = CodeSetFull.From (0x1A00, 0x1A1F);
                }
                return _buginese_;
            }
        }
        private static ICodeSet _buginese_;

        /// <summary>
        /// 1A20..1AAF; Tai Tham
        /// </summary>
        public static ICodeSet TaiTham {
            get {
                if (_taiTham_ == null) {
                    _taiTham_ = CodeSetFull.From (0x1A20, 0x1AAF);
                }
                return _taiTham_;
            }
        }
        private static ICodeSet _taiTham_;

        /// <summary>
        /// 1AB0..1AFF; Combining Diacritical Marks Extended
        /// </summary>
        public static ICodeSet CombiningDiacriticalMarksExtended {
            get {
                if (_combiningDiacriticalMarksExtended_ == null) {
                    _combiningDiacriticalMarksExtended_ = CodeSetFull.From (0x1AB0, 0x1AFF);
                }
                return _combiningDiacriticalMarksExtended_;
            }
        }
        private static ICodeSet _combiningDiacriticalMarksExtended_;

        /// <summary>
        /// 1B00..1B7F; Balinese
        /// </summary>
        public static ICodeSet Balinese {
            get {
                if (_balinese_ == null) {
                    _balinese_ = CodeSetFull.From (0x1B00, 0x1B7F);
                }
                return _balinese_;
            }
        }
        private static ICodeSet _balinese_;

        /// <summary>
        /// 1B80..1BBF; Sundanese
        /// </summary>
        public static ICodeSet Sundanese {
            get {
                if (_sundanese_ == null) {
                    _sundanese_ = CodeSetFull.From (0x1B80, 0x1BBF);
                }
                return _sundanese_;
            }
        }
        private static ICodeSet _sundanese_;

        /// <summary>
        /// 1BC0..1BFF; Batak
        /// </summary>
        public static ICodeSet Batak {
            get {
                if (_batak_ == null) {
                    _batak_ = CodeSetFull.From (0x1BC0, 0x1BFF);
                }
                return _batak_;
            }
        }
        private static ICodeSet _batak_;

        /// <summary>
        /// 1C00..1C4F; Lepcha
        /// </summary>
        public static ICodeSet Lepcha {
            get {
                if (_lepcha_ == null) {
                    _lepcha_ = CodeSetFull.From (0x1C00, 0x1C4F);
                }
                return _lepcha_;
            }
        }
        private static ICodeSet _lepcha_;

        /// <summary>
        /// 1C50..1C7F; Ol Chiki
        /// </summary>
        public static ICodeSet OlChiki {
            get {
                if (_olChiki_ == null) {
                    _olChiki_ = CodeSetFull.From (0x1C50, 0x1C7F);
                }
                return _olChiki_;
            }
        }
        private static ICodeSet _olChiki_;

        /// <summary>
        /// 1CC0..1CCF; Sundanese Supplement
        /// </summary>
        public static ICodeSet SundaneseSupplement {
            get {
                if (_sundaneseSupplement_ == null) {
                    _sundaneseSupplement_ = CodeSetFull.From (0x1CC0, 0x1CCF);
                }
                return _sundaneseSupplement_;
            }
        }
        private static ICodeSet _sundaneseSupplement_;

        /// <summary>
        /// 1CD0..1CFF; Vedic Extensions
        /// </summary>
        public static ICodeSet VedicExtensions {
            get {
                if (_vedicExtensions_ == null) {
                    _vedicExtensions_ = CodeSetFull.From (0x1CD0, 0x1CFF);
                }
                return _vedicExtensions_;
            }
        }
        private static ICodeSet _vedicExtensions_;

        /// <summary>
        /// 1D00..1D7F; Phonetic Extensions
        /// </summary>
        public static ICodeSet PhoneticExtensions {
            get {
                if (_phoneticExtensions_ == null) {
                    _phoneticExtensions_ = CodeSetFull.From (0x1D00, 0x1D7F);
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
                    _phoneticExtensionsSupplement_ = CodeSetFull.From (0x1D80, 0x1DBF);
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
                    _combiningDiacriticalMarksSupplement_ = CodeSetFull.From (0x1DC0, 0x1DFF);
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
                    _latinExtendedAdditional_ = CodeSetFull.From (0x1E00, 0x1EFF);
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
                    _greekExtended_ = CodeSetFull.From (0x1F00, 0x1FFF);
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
                    _generalPunctuation_ = CodeSetFull.From (0x2000, 0x206F);
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
                    _superscriptsandSubscripts_ = CodeSetFull.From (0x2070, 0x209F);
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
                    _currencySymbols_ = CodeSetFull.From (0x20A0, 0x20CF);
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
                    _combiningDiacriticalMarksforSymbols_ = CodeSetFull.From (0x20D0, 0x20FF);
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
                    _letterlikeSymbols_ = CodeSetFull.From (0x2100, 0x214F);
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
                    _numberForms_ = CodeSetFull.From (0x2150, 0x218F);
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
                    _arrows_ = CodeSetFull.From (0x2190, 0x21FF);
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
                    _mathematicalOperators_ = CodeSetFull.From (0x2200, 0x22FF);
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
                    _miscellaneousTechnical_ = CodeSetFull.From (0x2300, 0x23FF);
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
                    _controlPictures_ = CodeSetFull.From (0x2400, 0x243F);
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
                    _opticalCharacterRecognition_ = CodeSetFull.From (0x2440, 0x245F);
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
                    _enclosedAlphanumerics_ = CodeSetFull.From (0x2460, 0x24FF);
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
                    _boxDrawing_ = CodeSetFull.From (0x2500, 0x257F);
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
                    _blockElements_ = CodeSetFull.From (0x2580, 0x259F);
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
                    _geometricShapes_ = CodeSetFull.From (0x25A0, 0x25FF);
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
                    _miscellaneousSymbols_ = CodeSetFull.From (0x2600, 0x26FF);
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
                    _dingbats_ = CodeSetFull.From (0x2700, 0x27BF);
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
                    _miscellaneousMathematicalSymbolsA_ = CodeSetFull.From (0x27C0, 0x27EF);
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
                    _supplementalArrowsA_ = CodeSetFull.From (0x27F0, 0x27FF);
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
                    _braillePatterns_ = CodeSetFull.From (0x2800, 0x28FF);
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
                    _supplementalArrowsB_ = CodeSetFull.From (0x2900, 0x297F);
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
                    _miscellaneousMathematicalSymbolsB_ = CodeSetFull.From (0x2980, 0x29FF);
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
                    _supplementalMathematicalOperators_ = CodeSetFull.From (0x2A00, 0x2AFF);
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
                    _miscellaneousSymbolsandArrows_ = CodeSetFull.From (0x2B00, 0x2BFF);
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
                    _glagolitic_ = CodeSetFull.From (0x2C00, 0x2C5F);
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
                    _latinExtendedC_ = CodeSetFull.From (0x2C60, 0x2C7F);
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
                    _coptic_ = CodeSetFull.From (0x2C80, 0x2CFF);
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
                    _georgianSupplement_ = CodeSetFull.From (0x2D00, 0x2D2F);
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
                    _tifinagh_ = CodeSetFull.From (0x2D30, 0x2D7F);
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
                    _ethiopicExtended_ = CodeSetFull.From (0x2D80, 0x2DDF);
                }
                return _ethiopicExtended_;
            }
        }
        private static ICodeSet _ethiopicExtended_;

        /// <summary>
        /// 2DE0..2DFF; Cyrillic Extended-A
        /// </summary>
        public static ICodeSet CyrillicExtendedA {
            get {
                if (_cyrillicExtendedA_ == null) {
                    _cyrillicExtendedA_ = CodeSetFull.From (0x2DE0, 0x2DFF);
                }
                return _cyrillicExtendedA_;
            }
        }
        private static ICodeSet _cyrillicExtendedA_;

        /// <summary>
        /// 2E00..2E7F; Supplemental Punctuation
        /// </summary>
        public static ICodeSet SupplementalPunctuation {
            get {
                if (_supplementalPunctuation_ == null) {
                    _supplementalPunctuation_ = CodeSetFull.From (0x2E00, 0x2E7F);
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
                    _cJKRadicalsSupplement_ = CodeSetFull.From (0x2E80, 0x2EFF);
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
                    _kangxiRadicals_ = CodeSetFull.From (0x2F00, 0x2FDF);
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
                    _ideographicDescriptionCharacters_ = CodeSetFull.From (0x2FF0, 0x2FFF);
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
                    _cJKSymbolsandPunctuation_ = CodeSetFull.From (0x3000, 0x303F);
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
                    _hiragana_ = CodeSetFull.From (0x3040, 0x309F);
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
                    _katakana_ = CodeSetFull.From (0x30A0, 0x30FF);
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
                    _bopomofo_ = CodeSetFull.From (0x3100, 0x312F);
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
                    _hangulCompatibilityJamo_ = CodeSetFull.From (0x3130, 0x318F);
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
                    _kanbun_ = CodeSetFull.From (0x3190, 0x319F);
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
                    _bopomofoExtended_ = CodeSetFull.From (0x31A0, 0x31BF);
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
                    _cJKStrokes_ = CodeSetFull.From (0x31C0, 0x31EF);
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
                    _katakanaPhoneticExtensions_ = CodeSetFull.From (0x31F0, 0x31FF);
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
                    _enclosedCJKLettersandMonths_ = CodeSetFull.From (0x3200, 0x32FF);
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
                    _cJKCompatibility_ = CodeSetFull.From (0x3300, 0x33FF);
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
                    _cJKUnifiedIdeographsExtensionA_ = CodeSetFull.From (0x3400, 0x4DBF);
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
                    _yijingHexagramSymbols_ = CodeSetFull.From (0x4DC0, 0x4DFF);
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
                    _cJKUnifiedIdeographs_ = CodeSetFull.From (0x4E00, 0x9FFF);
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
                    _yiSyllables_ = CodeSetFull.From (0xA000, 0xA48F);
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
                    _yiRadicals_ = CodeSetFull.From (0xA490, 0xA4CF);
                }
                return _yiRadicals_;
            }
        }
        private static ICodeSet _yiRadicals_;

        /// <summary>
        /// A4D0..A4FF; Lisu
        /// </summary>
        public static ICodeSet Lisu {
            get {
                if (_lisu_ == null) {
                    _lisu_ = CodeSetFull.From (0xA4D0, 0xA4FF);
                }
                return _lisu_;
            }
        }
        private static ICodeSet _lisu_;

        /// <summary>
        /// A500..A63F; Vai
        /// </summary>
        public static ICodeSet Vai {
            get {
                if (_vai_ == null) {
                    _vai_ = CodeSetFull.From (0xA500, 0xA63F);
                }
                return _vai_;
            }
        }
        private static ICodeSet _vai_;

        /// <summary>
        /// A640..A69F; Cyrillic Extended-B
        /// </summary>
        public static ICodeSet CyrillicExtendedB {
            get {
                if (_cyrillicExtendedB_ == null) {
                    _cyrillicExtendedB_ = CodeSetFull.From (0xA640, 0xA69F);
                }
                return _cyrillicExtendedB_;
            }
        }
        private static ICodeSet _cyrillicExtendedB_;

        /// <summary>
        /// A6A0..A6FF; Bamum
        /// </summary>
        public static ICodeSet Bamum {
            get {
                if (_bamum_ == null) {
                    _bamum_ = CodeSetFull.From (0xA6A0, 0xA6FF);
                }
                return _bamum_;
            }
        }
        private static ICodeSet _bamum_;

        /// <summary>
        /// A700..A71F; Modifier Tone Letters
        /// </summary>
        public static ICodeSet ModifierToneLetters {
            get {
                if (_modifierToneLetters_ == null) {
                    _modifierToneLetters_ = CodeSetFull.From (0xA700, 0xA71F);
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
                    _latinExtendedD_ = CodeSetFull.From (0xA720, 0xA7FF);
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
                    _sylotiNagri_ = CodeSetFull.From (0xA800, 0xA82F);
                }
                return _sylotiNagri_;
            }
        }
        private static ICodeSet _sylotiNagri_;

        /// <summary>
        /// A830..A83F; Common Indic Number Forms
        /// </summary>
        public static ICodeSet CommonIndicNumberForms {
            get {
                if (_commonIndicNumberForms_ == null) {
                    _commonIndicNumberForms_ = CodeSetFull.From (0xA830, 0xA83F);
                }
                return _commonIndicNumberForms_;
            }
        }
        private static ICodeSet _commonIndicNumberForms_;

        /// <summary>
        /// A840..A87F; Phags-pa
        /// </summary>
        public static ICodeSet Phagspa {
            get {
                if (_phagspa_ == null) {
                    _phagspa_ = CodeSetFull.From (0xA840, 0xA87F);
                }
                return _phagspa_;
            }
        }
        private static ICodeSet _phagspa_;

        /// <summary>
        /// A880..A8DF; Saurashtra
        /// </summary>
        public static ICodeSet Saurashtra {
            get {
                if (_saurashtra_ == null) {
                    _saurashtra_ = CodeSetFull.From (0xA880, 0xA8DF);
                }
                return _saurashtra_;
            }
        }
        private static ICodeSet _saurashtra_;

        /// <summary>
        /// A8E0..A8FF; Devanagari Extended
        /// </summary>
        public static ICodeSet DevanagariExtended {
            get {
                if (_devanagariExtended_ == null) {
                    _devanagariExtended_ = CodeSetFull.From (0xA8E0, 0xA8FF);
                }
                return _devanagariExtended_;
            }
        }
        private static ICodeSet _devanagariExtended_;

        /// <summary>
        /// A900..A92F; Kayah Li
        /// </summary>
        public static ICodeSet KayahLi {
            get {
                if (_kayahLi_ == null) {
                    _kayahLi_ = CodeSetFull.From (0xA900, 0xA92F);
                }
                return _kayahLi_;
            }
        }
        private static ICodeSet _kayahLi_;

        /// <summary>
        /// A930..A95F; Rejang
        /// </summary>
        public static ICodeSet Rejang {
            get {
                if (_rejang_ == null) {
                    _rejang_ = CodeSetFull.From (0xA930, 0xA95F);
                }
                return _rejang_;
            }
        }
        private static ICodeSet _rejang_;

        /// <summary>
        /// A960..A97F; Hangul Jamo Extended-A
        /// </summary>
        public static ICodeSet HangulJamoExtendedA {
            get {
                if (_hangulJamoExtendedA_ == null) {
                    _hangulJamoExtendedA_ = CodeSetFull.From (0xA960, 0xA97F);
                }
                return _hangulJamoExtendedA_;
            }
        }
        private static ICodeSet _hangulJamoExtendedA_;

        /// <summary>
        /// A980..A9DF; Javanese
        /// </summary>
        public static ICodeSet Javanese {
            get {
                if (_javanese_ == null) {
                    _javanese_ = CodeSetFull.From (0xA980, 0xA9DF);
                }
                return _javanese_;
            }
        }
        private static ICodeSet _javanese_;

        /// <summary>
        /// A9E0..A9FF; Myanmar Extended-B
        /// </summary>
        public static ICodeSet MyanmarExtendedB {
            get {
                if (_myanmarExtendedB_ == null) {
                    _myanmarExtendedB_ = CodeSetFull.From (0xA9E0, 0xA9FF);
                }
                return _myanmarExtendedB_;
            }
        }
        private static ICodeSet _myanmarExtendedB_;

        /// <summary>
        /// AA00..AA5F; Cham
        /// </summary>
        public static ICodeSet Cham {
            get {
                if (_cham_ == null) {
                    _cham_ = CodeSetFull.From (0xAA00, 0xAA5F);
                }
                return _cham_;
            }
        }
        private static ICodeSet _cham_;

        /// <summary>
        /// AA60..AA7F; Myanmar Extended-A
        /// </summary>
        public static ICodeSet MyanmarExtendedA {
            get {
                if (_myanmarExtendedA_ == null) {
                    _myanmarExtendedA_ = CodeSetFull.From (0xAA60, 0xAA7F);
                }
                return _myanmarExtendedA_;
            }
        }
        private static ICodeSet _myanmarExtendedA_;

        /// <summary>
        /// AA80..AADF; Tai Viet
        /// </summary>
        public static ICodeSet TaiViet {
            get {
                if (_taiViet_ == null) {
                    _taiViet_ = CodeSetFull.From (0xAA80, 0xAADF);
                }
                return _taiViet_;
            }
        }
        private static ICodeSet _taiViet_;

        /// <summary>
        /// AAE0..AAFF; Meetei Mayek Extensions
        /// </summary>
        public static ICodeSet MeeteiMayekExtensions {
            get {
                if (_meeteiMayekExtensions_ == null) {
                    _meeteiMayekExtensions_ = CodeSetFull.From (0xAAE0, 0xAAFF);
                }
                return _meeteiMayekExtensions_;
            }
        }
        private static ICodeSet _meeteiMayekExtensions_;

        /// <summary>
        /// AB00..AB2F; Ethiopic Extended-A
        /// </summary>
        public static ICodeSet EthiopicExtendedA {
            get {
                if (_ethiopicExtendedA_ == null) {
                    _ethiopicExtendedA_ = CodeSetFull.From (0xAB00, 0xAB2F);
                }
                return _ethiopicExtendedA_;
            }
        }
        private static ICodeSet _ethiopicExtendedA_;

        /// <summary>
        /// AB30..AB6F; Latin Extended-E
        /// </summary>
        public static ICodeSet LatinExtendedE {
            get {
                if (_latinExtendedE_ == null) {
                    _latinExtendedE_ = CodeSetFull.From (0xAB30, 0xAB6F);
                }
                return _latinExtendedE_;
            }
        }
        private static ICodeSet _latinExtendedE_;

        /// <summary>
        /// AB70..ABBF; Cherokee Supplement
        /// </summary>
        public static ICodeSet CherokeeSupplement {
            get {
                if (_cherokeeSupplement_ == null) {
                    _cherokeeSupplement_ = CodeSetFull.From (0xAB70, 0xABBF);
                }
                return _cherokeeSupplement_;
            }
        }
        private static ICodeSet _cherokeeSupplement_;

        /// <summary>
        /// ABC0..ABFF; Meetei Mayek
        /// </summary>
        public static ICodeSet MeeteiMayek {
            get {
                if (_meeteiMayek_ == null) {
                    _meeteiMayek_ = CodeSetFull.From (0xABC0, 0xABFF);
                }
                return _meeteiMayek_;
            }
        }
        private static ICodeSet _meeteiMayek_;

        /// <summary>
        /// AC00..D7AF; Hangul Syllables
        /// </summary>
        public static ICodeSet HangulSyllables {
            get {
                if (_hangulSyllables_ == null) {
                    _hangulSyllables_ = CodeSetFull.From (0xAC00, 0xD7AF);
                }
                return _hangulSyllables_;
            }
        }
        private static ICodeSet _hangulSyllables_;

        /// <summary>
        /// D7B0..D7FF; Hangul Jamo Extended-B
        /// </summary>
        public static ICodeSet HangulJamoExtendedB {
            get {
                if (_hangulJamoExtendedB_ == null) {
                    _hangulJamoExtendedB_ = CodeSetFull.From (0xD7B0, 0xD7FF);
                }
                return _hangulJamoExtendedB_;
            }
        }
        private static ICodeSet _hangulJamoExtendedB_;

        /// <summary>
        /// D800..DB7F; High Surrogates
        /// </summary>
        public static ICodeSet HighSurrogates {
            get {
                if (_highSurrogates_ == null) {
                    _highSurrogates_ = CodeSetFull.From (0xD800, 0xDB7F);
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
                    _highPrivateUseSurrogates_ = CodeSetFull.From (0xDB80, 0xDBFF);
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
                    _lowSurrogates_ = CodeSetFull.From (0xDC00, 0xDFFF);
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
                    _privateUseArea_ = CodeSetFull.From (0xE000, 0xF8FF);
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
                    _cJKCompatibilityIdeographs_ = CodeSetFull.From (0xF900, 0xFAFF);
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
                    _alphabeticPresentationForms_ = CodeSetFull.From (0xFB00, 0xFB4F);
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
                    _arabicPresentationFormsA_ = CodeSetFull.From (0xFB50, 0xFDFF);
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
                    _variationSelectors_ = CodeSetFull.From (0xFE00, 0xFE0F);
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
                    _verticalForms_ = CodeSetFull.From (0xFE10, 0xFE1F);
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
                    _combiningHalfMarks_ = CodeSetFull.From (0xFE20, 0xFE2F);
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
                    _cJKCompatibilityForms_ = CodeSetFull.From (0xFE30, 0xFE4F);
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
                    _smallFormVariants_ = CodeSetFull.From (0xFE50, 0xFE6F);
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
                    _arabicPresentationFormsB_ = CodeSetFull.From (0xFE70, 0xFEFF);
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
                    _halfwidthandFullwidthForms_ = CodeSetFull.From (0xFF00, 0xFFEF);
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
                    _specials_ = CodeSetFull.From (0xFFF0, 0xFFFF);
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
                    _linearBSyllabary_ = CodeSetFull.From (0x10000, 0x1007F);
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
                    _linearBIdeograms_ = CodeSetFull.From (0x10080, 0x100FF);
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
                    _aegeanNumbers_ = CodeSetFull.From (0x10100, 0x1013F);
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
                    _ancientGreekNumbers_ = CodeSetFull.From (0x10140, 0x1018F);
                }
                return _ancientGreekNumbers_;
            }
        }
        private static ICodeSet _ancientGreekNumbers_;

        /// <summary>
        /// 10190..101CF; Ancient Symbols
        /// </summary>
        public static ICodeSet AncientSymbols {
            get {
                if (_ancientSymbols_ == null) {
                    _ancientSymbols_ = CodeSetFull.From (0x10190, 0x101CF);
                }
                return _ancientSymbols_;
            }
        }
        private static ICodeSet _ancientSymbols_;

        /// <summary>
        /// 101D0..101FF; Phaistos Disc
        /// </summary>
        public static ICodeSet PhaistosDisc {
            get {
                if (_phaistosDisc_ == null) {
                    _phaistosDisc_ = CodeSetFull.From (0x101D0, 0x101FF);
                }
                return _phaistosDisc_;
            }
        }
        private static ICodeSet _phaistosDisc_;

        /// <summary>
        /// 10280..1029F; Lycian
        /// </summary>
        public static ICodeSet Lycian {
            get {
                if (_lycian_ == null) {
                    _lycian_ = CodeSetFull.From (0x10280, 0x1029F);
                }
                return _lycian_;
            }
        }
        private static ICodeSet _lycian_;

        /// <summary>
        /// 102A0..102DF; Carian
        /// </summary>
        public static ICodeSet Carian {
            get {
                if (_carian_ == null) {
                    _carian_ = CodeSetFull.From (0x102A0, 0x102DF);
                }
                return _carian_;
            }
        }
        private static ICodeSet _carian_;

        /// <summary>
        /// 102E0..102FF; Coptic Epact Numbers
        /// </summary>
        public static ICodeSet CopticEpactNumbers {
            get {
                if (_copticEpactNumbers_ == null) {
                    _copticEpactNumbers_ = CodeSetFull.From (0x102E0, 0x102FF);
                }
                return _copticEpactNumbers_;
            }
        }
        private static ICodeSet _copticEpactNumbers_;

        /// <summary>
        /// 10300..1032F; Old Italic
        /// </summary>
        public static ICodeSet OldItalic {
            get {
                if (_oldItalic_ == null) {
                    _oldItalic_ = CodeSetFull.From (0x10300, 0x1032F);
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
                    _gothic_ = CodeSetFull.From (0x10330, 0x1034F);
                }
                return _gothic_;
            }
        }
        private static ICodeSet _gothic_;

        /// <summary>
        /// 10350..1037F; Old Permic
        /// </summary>
        public static ICodeSet OldPermic {
            get {
                if (_oldPermic_ == null) {
                    _oldPermic_ = CodeSetFull.From (0x10350, 0x1037F);
                }
                return _oldPermic_;
            }
        }
        private static ICodeSet _oldPermic_;

        /// <summary>
        /// 10380..1039F; Ugaritic
        /// </summary>
        public static ICodeSet Ugaritic {
            get {
                if (_ugaritic_ == null) {
                    _ugaritic_ = CodeSetFull.From (0x10380, 0x1039F);
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
                    _oldPersian_ = CodeSetFull.From (0x103A0, 0x103DF);
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
                    _deseret_ = CodeSetFull.From (0x10400, 0x1044F);
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
                    _shavian_ = CodeSetFull.From (0x10450, 0x1047F);
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
                    _osmanya_ = CodeSetFull.From (0x10480, 0x104AF);
                }
                return _osmanya_;
            }
        }
        private static ICodeSet _osmanya_;

        /// <summary>
        /// 10500..1052F; Elbasan
        /// </summary>
        public static ICodeSet Elbasan {
            get {
                if (_elbasan_ == null) {
                    _elbasan_ = CodeSetFull.From (0x10500, 0x1052F);
                }
                return _elbasan_;
            }
        }
        private static ICodeSet _elbasan_;

        /// <summary>
        /// 10530..1056F; Caucasian Albanian
        /// </summary>
        public static ICodeSet CaucasianAlbanian {
            get {
                if (_caucasianAlbanian_ == null) {
                    _caucasianAlbanian_ = CodeSetFull.From (0x10530, 0x1056F);
                }
                return _caucasianAlbanian_;
            }
        }
        private static ICodeSet _caucasianAlbanian_;

        /// <summary>
        /// 10600..1077F; Linear A
        /// </summary>
        public static ICodeSet LinearA {
            get {
                if (_linearA_ == null) {
                    _linearA_ = CodeSetFull.From (0x10600, 0x1077F);
                }
                return _linearA_;
            }
        }
        private static ICodeSet _linearA_;

        /// <summary>
        /// 10800..1083F; Cypriot Syllabary
        /// </summary>
        public static ICodeSet CypriotSyllabary {
            get {
                if (_cypriotSyllabary_ == null) {
                    _cypriotSyllabary_ = CodeSetFull.From (0x10800, 0x1083F);
                }
                return _cypriotSyllabary_;
            }
        }
        private static ICodeSet _cypriotSyllabary_;

        /// <summary>
        /// 10840..1085F; Imperial Aramaic
        /// </summary>
        public static ICodeSet ImperialAramaic {
            get {
                if (_imperialAramaic_ == null) {
                    _imperialAramaic_ = CodeSetFull.From (0x10840, 0x1085F);
                }
                return _imperialAramaic_;
            }
        }
        private static ICodeSet _imperialAramaic_;

        /// <summary>
        /// 10860..1087F; Palmyrene
        /// </summary>
        public static ICodeSet Palmyrene {
            get {
                if (_palmyrene_ == null) {
                    _palmyrene_ = CodeSetFull.From (0x10860, 0x1087F);
                }
                return _palmyrene_;
            }
        }
        private static ICodeSet _palmyrene_;

        /// <summary>
        /// 10880..108AF; Nabataean
        /// </summary>
        public static ICodeSet Nabataean {
            get {
                if (_nabataean_ == null) {
                    _nabataean_ = CodeSetFull.From (0x10880, 0x108AF);
                }
                return _nabataean_;
            }
        }
        private static ICodeSet _nabataean_;

        /// <summary>
        /// 108E0..108FF; Hatran
        /// </summary>
        public static ICodeSet Hatran {
            get {
                if (_hatran_ == null) {
                    _hatran_ = CodeSetFull.From (0x108E0, 0x108FF);
                }
                return _hatran_;
            }
        }
        private static ICodeSet _hatran_;

        /// <summary>
        /// 10900..1091F; Phoenician
        /// </summary>
        public static ICodeSet Phoenician {
            get {
                if (_phoenician_ == null) {
                    _phoenician_ = CodeSetFull.From (0x10900, 0x1091F);
                }
                return _phoenician_;
            }
        }
        private static ICodeSet _phoenician_;

        /// <summary>
        /// 10920..1093F; Lydian
        /// </summary>
        public static ICodeSet Lydian {
            get {
                if (_lydian_ == null) {
                    _lydian_ = CodeSetFull.From (0x10920, 0x1093F);
                }
                return _lydian_;
            }
        }
        private static ICodeSet _lydian_;

        /// <summary>
        /// 10980..1099F; Meroitic Hieroglyphs
        /// </summary>
        public static ICodeSet MeroiticHieroglyphs {
            get {
                if (_meroiticHieroglyphs_ == null) {
                    _meroiticHieroglyphs_ = CodeSetFull.From (0x10980, 0x1099F);
                }
                return _meroiticHieroglyphs_;
            }
        }
        private static ICodeSet _meroiticHieroglyphs_;

        /// <summary>
        /// 109A0..109FF; Meroitic Cursive
        /// </summary>
        public static ICodeSet MeroiticCursive {
            get {
                if (_meroiticCursive_ == null) {
                    _meroiticCursive_ = CodeSetFull.From (0x109A0, 0x109FF);
                }
                return _meroiticCursive_;
            }
        }
        private static ICodeSet _meroiticCursive_;

        /// <summary>
        /// 10A00..10A5F; Kharoshthi
        /// </summary>
        public static ICodeSet Kharoshthi {
            get {
                if (_kharoshthi_ == null) {
                    _kharoshthi_ = CodeSetFull.From (0x10A00, 0x10A5F);
                }
                return _kharoshthi_;
            }
        }
        private static ICodeSet _kharoshthi_;

        /// <summary>
        /// 10A60..10A7F; Old South Arabian
        /// </summary>
        public static ICodeSet OldSouthArabian {
            get {
                if (_oldSouthArabian_ == null) {
                    _oldSouthArabian_ = CodeSetFull.From (0x10A60, 0x10A7F);
                }
                return _oldSouthArabian_;
            }
        }
        private static ICodeSet _oldSouthArabian_;

        /// <summary>
        /// 10A80..10A9F; Old North Arabian
        /// </summary>
        public static ICodeSet OldNorthArabian {
            get {
                if (_oldNorthArabian_ == null) {
                    _oldNorthArabian_ = CodeSetFull.From (0x10A80, 0x10A9F);
                }
                return _oldNorthArabian_;
            }
        }
        private static ICodeSet _oldNorthArabian_;

        /// <summary>
        /// 10AC0..10AFF; Manichaean
        /// </summary>
        public static ICodeSet Manichaean {
            get {
                if (_manichaean_ == null) {
                    _manichaean_ = CodeSetFull.From (0x10AC0, 0x10AFF);
                }
                return _manichaean_;
            }
        }
        private static ICodeSet _manichaean_;

        /// <summary>
        /// 10B00..10B3F; Avestan
        /// </summary>
        public static ICodeSet Avestan {
            get {
                if (_avestan_ == null) {
                    _avestan_ = CodeSetFull.From (0x10B00, 0x10B3F);
                }
                return _avestan_;
            }
        }
        private static ICodeSet _avestan_;

        /// <summary>
        /// 10B40..10B5F; Inscriptional Parthian
        /// </summary>
        public static ICodeSet InscriptionalParthian {
            get {
                if (_inscriptionalParthian_ == null) {
                    _inscriptionalParthian_ = CodeSetFull.From (0x10B40, 0x10B5F);
                }
                return _inscriptionalParthian_;
            }
        }
        private static ICodeSet _inscriptionalParthian_;

        /// <summary>
        /// 10B60..10B7F; Inscriptional Pahlavi
        /// </summary>
        public static ICodeSet InscriptionalPahlavi {
            get {
                if (_inscriptionalPahlavi_ == null) {
                    _inscriptionalPahlavi_ = CodeSetFull.From (0x10B60, 0x10B7F);
                }
                return _inscriptionalPahlavi_;
            }
        }
        private static ICodeSet _inscriptionalPahlavi_;

        /// <summary>
        /// 10B80..10BAF; Psalter Pahlavi
        /// </summary>
        public static ICodeSet PsalterPahlavi {
            get {
                if (_psalterPahlavi_ == null) {
                    _psalterPahlavi_ = CodeSetFull.From (0x10B80, 0x10BAF);
                }
                return _psalterPahlavi_;
            }
        }
        private static ICodeSet _psalterPahlavi_;

        /// <summary>
        /// 10C00..10C4F; Old Turkic
        /// </summary>
        public static ICodeSet OldTurkic {
            get {
                if (_oldTurkic_ == null) {
                    _oldTurkic_ = CodeSetFull.From (0x10C00, 0x10C4F);
                }
                return _oldTurkic_;
            }
        }
        private static ICodeSet _oldTurkic_;

        /// <summary>
        /// 10C80..10CFF; Old Hungarian
        /// </summary>
        public static ICodeSet OldHungarian {
            get {
                if (_oldHungarian_ == null) {
                    _oldHungarian_ = CodeSetFull.From (0x10C80, 0x10CFF);
                }
                return _oldHungarian_;
            }
        }
        private static ICodeSet _oldHungarian_;

        /// <summary>
        /// 10E60..10E7F; Rumi Numeral Symbols
        /// </summary>
        public static ICodeSet RumiNumeralSymbols {
            get {
                if (_rumiNumeralSymbols_ == null) {
                    _rumiNumeralSymbols_ = CodeSetFull.From (0x10E60, 0x10E7F);
                }
                return _rumiNumeralSymbols_;
            }
        }
        private static ICodeSet _rumiNumeralSymbols_;

        /// <summary>
        /// 11000..1107F; Brahmi
        /// </summary>
        public static ICodeSet Brahmi {
            get {
                if (_brahmi_ == null) {
                    _brahmi_ = CodeSetFull.From (0x11000, 0x1107F);
                }
                return _brahmi_;
            }
        }
        private static ICodeSet _brahmi_;

        /// <summary>
        /// 11080..110CF; Kaithi
        /// </summary>
        public static ICodeSet Kaithi {
            get {
                if (_kaithi_ == null) {
                    _kaithi_ = CodeSetFull.From (0x11080, 0x110CF);
                }
                return _kaithi_;
            }
        }
        private static ICodeSet _kaithi_;

        /// <summary>
        /// 110D0..110FF; Sora Sompeng
        /// </summary>
        public static ICodeSet SoraSompeng {
            get {
                if (_soraSompeng_ == null) {
                    _soraSompeng_ = CodeSetFull.From (0x110D0, 0x110FF);
                }
                return _soraSompeng_;
            }
        }
        private static ICodeSet _soraSompeng_;

        /// <summary>
        /// 11100..1114F; Chakma
        /// </summary>
        public static ICodeSet Chakma {
            get {
                if (_chakma_ == null) {
                    _chakma_ = CodeSetFull.From (0x11100, 0x1114F);
                }
                return _chakma_;
            }
        }
        private static ICodeSet _chakma_;

        /// <summary>
        /// 11150..1117F; Mahajani
        /// </summary>
        public static ICodeSet Mahajani {
            get {
                if (_mahajani_ == null) {
                    _mahajani_ = CodeSetFull.From (0x11150, 0x1117F);
                }
                return _mahajani_;
            }
        }
        private static ICodeSet _mahajani_;

        /// <summary>
        /// 11180..111DF; Sharada
        /// </summary>
        public static ICodeSet Sharada {
            get {
                if (_sharada_ == null) {
                    _sharada_ = CodeSetFull.From (0x11180, 0x111DF);
                }
                return _sharada_;
            }
        }
        private static ICodeSet _sharada_;

        /// <summary>
        /// 111E0..111FF; Sinhala Archaic Numbers
        /// </summary>
        public static ICodeSet SinhalaArchaicNumbers {
            get {
                if (_sinhalaArchaicNumbers_ == null) {
                    _sinhalaArchaicNumbers_ = CodeSetFull.From (0x111E0, 0x111FF);
                }
                return _sinhalaArchaicNumbers_;
            }
        }
        private static ICodeSet _sinhalaArchaicNumbers_;

        /// <summary>
        /// 11200..1124F; Khojki
        /// </summary>
        public static ICodeSet Khojki {
            get {
                if (_khojki_ == null) {
                    _khojki_ = CodeSetFull.From (0x11200, 0x1124F);
                }
                return _khojki_;
            }
        }
        private static ICodeSet _khojki_;

        /// <summary>
        /// 11280..112AF; Multani
        /// </summary>
        public static ICodeSet Multani {
            get {
                if (_multani_ == null) {
                    _multani_ = CodeSetFull.From (0x11280, 0x112AF);
                }
                return _multani_;
            }
        }
        private static ICodeSet _multani_;

        /// <summary>
        /// 112B0..112FF; Khudawadi
        /// </summary>
        public static ICodeSet Khudawadi {
            get {
                if (_khudawadi_ == null) {
                    _khudawadi_ = CodeSetFull.From (0x112B0, 0x112FF);
                }
                return _khudawadi_;
            }
        }
        private static ICodeSet _khudawadi_;

        /// <summary>
        /// 11300..1137F; Grantha
        /// </summary>
        public static ICodeSet Grantha {
            get {
                if (_grantha_ == null) {
                    _grantha_ = CodeSetFull.From (0x11300, 0x1137F);
                }
                return _grantha_;
            }
        }
        private static ICodeSet _grantha_;

        /// <summary>
        /// 11480..114DF; Tirhuta
        /// </summary>
        public static ICodeSet Tirhuta {
            get {
                if (_tirhuta_ == null) {
                    _tirhuta_ = CodeSetFull.From (0x11480, 0x114DF);
                }
                return _tirhuta_;
            }
        }
        private static ICodeSet _tirhuta_;

        /// <summary>
        /// 11580..115FF; Siddham
        /// </summary>
        public static ICodeSet Siddham {
            get {
                if (_siddham_ == null) {
                    _siddham_ = CodeSetFull.From (0x11580, 0x115FF);
                }
                return _siddham_;
            }
        }
        private static ICodeSet _siddham_;

        /// <summary>
        /// 11600..1165F; Modi
        /// </summary>
        public static ICodeSet Modi {
            get {
                if (_modi_ == null) {
                    _modi_ = CodeSetFull.From (0x11600, 0x1165F);
                }
                return _modi_;
            }
        }
        private static ICodeSet _modi_;

        /// <summary>
        /// 11680..116CF; Takri
        /// </summary>
        public static ICodeSet Takri {
            get {
                if (_takri_ == null) {
                    _takri_ = CodeSetFull.From (0x11680, 0x116CF);
                }
                return _takri_;
            }
        }
        private static ICodeSet _takri_;

        /// <summary>
        /// 11700..1173F; Ahom
        /// </summary>
        public static ICodeSet Ahom {
            get {
                if (_ahom_ == null) {
                    _ahom_ = CodeSetFull.From (0x11700, 0x1173F);
                }
                return _ahom_;
            }
        }
        private static ICodeSet _ahom_;

        /// <summary>
        /// 118A0..118FF; Warang Citi
        /// </summary>
        public static ICodeSet WarangCiti {
            get {
                if (_warangCiti_ == null) {
                    _warangCiti_ = CodeSetFull.From (0x118A0, 0x118FF);
                }
                return _warangCiti_;
            }
        }
        private static ICodeSet _warangCiti_;

        /// <summary>
        /// 11AC0..11AFF; Pau Cin Hau
        /// </summary>
        public static ICodeSet PauCinHau {
            get {
                if (_pauCinHau_ == null) {
                    _pauCinHau_ = CodeSetFull.From (0x11AC0, 0x11AFF);
                }
                return _pauCinHau_;
            }
        }
        private static ICodeSet _pauCinHau_;

        /// <summary>
        /// 12000..123FF; Cuneiform
        /// </summary>
        public static ICodeSet Cuneiform {
            get {
                if (_cuneiform_ == null) {
                    _cuneiform_ = CodeSetFull.From (0x12000, 0x123FF);
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
                    _cuneiformNumbersandPunctuation_ = CodeSetFull.From (0x12400, 0x1247F);
                }
                return _cuneiformNumbersandPunctuation_;
            }
        }
        private static ICodeSet _cuneiformNumbersandPunctuation_;

        /// <summary>
        /// 12480..1254F; Early Dynastic Cuneiform
        /// </summary>
        public static ICodeSet EarlyDynasticCuneiform {
            get {
                if (_earlyDynasticCuneiform_ == null) {
                    _earlyDynasticCuneiform_ = CodeSetFull.From (0x12480, 0x1254F);
                }
                return _earlyDynasticCuneiform_;
            }
        }
        private static ICodeSet _earlyDynasticCuneiform_;

        /// <summary>
        /// 13000..1342F; Egyptian Hieroglyphs
        /// </summary>
        public static ICodeSet EgyptianHieroglyphs {
            get {
                if (_egyptianHieroglyphs_ == null) {
                    _egyptianHieroglyphs_ = CodeSetFull.From (0x13000, 0x1342F);
                }
                return _egyptianHieroglyphs_;
            }
        }
        private static ICodeSet _egyptianHieroglyphs_;

        /// <summary>
        /// 14400..1467F; Anatolian Hieroglyphs
        /// </summary>
        public static ICodeSet AnatolianHieroglyphs {
            get {
                if (_anatolianHieroglyphs_ == null) {
                    _anatolianHieroglyphs_ = CodeSetFull.From (0x14400, 0x1467F);
                }
                return _anatolianHieroglyphs_;
            }
        }
        private static ICodeSet _anatolianHieroglyphs_;

        /// <summary>
        /// 16800..16A3F; Bamum Supplement
        /// </summary>
        public static ICodeSet BamumSupplement {
            get {
                if (_bamumSupplement_ == null) {
                    _bamumSupplement_ = CodeSetFull.From (0x16800, 0x16A3F);
                }
                return _bamumSupplement_;
            }
        }
        private static ICodeSet _bamumSupplement_;

        /// <summary>
        /// 16A40..16A6F; Mro
        /// </summary>
        public static ICodeSet Mro {
            get {
                if (_mro_ == null) {
                    _mro_ = CodeSetFull.From (0x16A40, 0x16A6F);
                }
                return _mro_;
            }
        }
        private static ICodeSet _mro_;

        /// <summary>
        /// 16AD0..16AFF; Bassa Vah
        /// </summary>
        public static ICodeSet BassaVah {
            get {
                if (_bassaVah_ == null) {
                    _bassaVah_ = CodeSetFull.From (0x16AD0, 0x16AFF);
                }
                return _bassaVah_;
            }
        }
        private static ICodeSet _bassaVah_;

        /// <summary>
        /// 16B00..16B8F; Pahawh Hmong
        /// </summary>
        public static ICodeSet PahawhHmong {
            get {
                if (_pahawhHmong_ == null) {
                    _pahawhHmong_ = CodeSetFull.From (0x16B00, 0x16B8F);
                }
                return _pahawhHmong_;
            }
        }
        private static ICodeSet _pahawhHmong_;

        /// <summary>
        /// 16F00..16F9F; Miao
        /// </summary>
        public static ICodeSet Miao {
            get {
                if (_miao_ == null) {
                    _miao_ = CodeSetFull.From (0x16F00, 0x16F9F);
                }
                return _miao_;
            }
        }
        private static ICodeSet _miao_;

        /// <summary>
        /// 1B000..1B0FF; Kana Supplement
        /// </summary>
        public static ICodeSet KanaSupplement {
            get {
                if (_kanaSupplement_ == null) {
                    _kanaSupplement_ = CodeSetFull.From (0x1B000, 0x1B0FF);
                }
                return _kanaSupplement_;
            }
        }
        private static ICodeSet _kanaSupplement_;

        /// <summary>
        /// 1BC00..1BC9F; Duployan
        /// </summary>
        public static ICodeSet Duployan {
            get {
                if (_duployan_ == null) {
                    _duployan_ = CodeSetFull.From (0x1BC00, 0x1BC9F);
                }
                return _duployan_;
            }
        }
        private static ICodeSet _duployan_;

        /// <summary>
        /// 1BCA0..1BCAF; Shorthand Format Controls
        /// </summary>
        public static ICodeSet ShorthandFormatControls {
            get {
                if (_shorthandFormatControls_ == null) {
                    _shorthandFormatControls_ = CodeSetFull.From (0x1BCA0, 0x1BCAF);
                }
                return _shorthandFormatControls_;
            }
        }
        private static ICodeSet _shorthandFormatControls_;

        /// <summary>
        /// 1D000..1D0FF; Byzantine Musical Symbols
        /// </summary>
        public static ICodeSet ByzantineMusicalSymbols {
            get {
                if (_byzantineMusicalSymbols_ == null) {
                    _byzantineMusicalSymbols_ = CodeSetFull.From (0x1D000, 0x1D0FF);
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
                    _musicalSymbols_ = CodeSetFull.From (0x1D100, 0x1D1FF);
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
                    _ancientGreekMusicalNotation_ = CodeSetFull.From (0x1D200, 0x1D24F);
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
                    _taiXuanJingSymbols_ = CodeSetFull.From (0x1D300, 0x1D35F);
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
                    _countingRodNumerals_ = CodeSetFull.From (0x1D360, 0x1D37F);
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
                    _mathematicalAlphanumericSymbols_ = CodeSetFull.From (0x1D400, 0x1D7FF);
                }
                return _mathematicalAlphanumericSymbols_;
            }
        }
        private static ICodeSet _mathematicalAlphanumericSymbols_;

        /// <summary>
        /// 1D800..1DAAF; Sutton SignWriting
        /// </summary>
        public static ICodeSet SuttonSignWriting {
            get {
                if (_suttonSignWriting_ == null) {
                    _suttonSignWriting_ = CodeSetFull.From (0x1D800, 0x1DAAF);
                }
                return _suttonSignWriting_;
            }
        }
        private static ICodeSet _suttonSignWriting_;

        /// <summary>
        /// 1E800..1E8DF; Mende Kikakui
        /// </summary>
        public static ICodeSet MendeKikakui {
            get {
                if (_mendeKikakui_ == null) {
                    _mendeKikakui_ = CodeSetFull.From (0x1E800, 0x1E8DF);
                }
                return _mendeKikakui_;
            }
        }
        private static ICodeSet _mendeKikakui_;

        /// <summary>
        /// 1EE00..1EEFF; Arabic Mathematical Alphabetic Symbols
        /// </summary>
        public static ICodeSet ArabicMathematicalAlphabeticSymbols {
            get {
                if (_arabicMathematicalAlphabeticSymbols_ == null) {
                    _arabicMathematicalAlphabeticSymbols_ = CodeSetFull.From (0x1EE00, 0x1EEFF);
                }
                return _arabicMathematicalAlphabeticSymbols_;
            }
        }
        private static ICodeSet _arabicMathematicalAlphabeticSymbols_;

        /// <summary>
        /// 1F000..1F02F; Mahjong Tiles
        /// </summary>
        public static ICodeSet MahjongTiles {
            get {
                if (_mahjongTiles_ == null) {
                    _mahjongTiles_ = CodeSetFull.From (0x1F000, 0x1F02F);
                }
                return _mahjongTiles_;
            }
        }
        private static ICodeSet _mahjongTiles_;

        /// <summary>
        /// 1F030..1F09F; Domino Tiles
        /// </summary>
        public static ICodeSet DominoTiles {
            get {
                if (_dominoTiles_ == null) {
                    _dominoTiles_ = CodeSetFull.From (0x1F030, 0x1F09F);
                }
                return _dominoTiles_;
            }
        }
        private static ICodeSet _dominoTiles_;

        /// <summary>
        /// 1F0A0..1F0FF; Playing Cards
        /// </summary>
        public static ICodeSet PlayingCards {
            get {
                if (_playingCards_ == null) {
                    _playingCards_ = CodeSetFull.From (0x1F0A0, 0x1F0FF);
                }
                return _playingCards_;
            }
        }
        private static ICodeSet _playingCards_;

        /// <summary>
        /// 1F100..1F1FF; Enclosed Alphanumeric Supplement
        /// </summary>
        public static ICodeSet EnclosedAlphanumericSupplement {
            get {
                if (_enclosedAlphanumericSupplement_ == null) {
                    _enclosedAlphanumericSupplement_ = CodeSetFull.From (0x1F100, 0x1F1FF);
                }
                return _enclosedAlphanumericSupplement_;
            }
        }
        private static ICodeSet _enclosedAlphanumericSupplement_;

        /// <summary>
        /// 1F200..1F2FF; Enclosed Ideographic Supplement
        /// </summary>
        public static ICodeSet EnclosedIdeographicSupplement {
            get {
                if (_enclosedIdeographicSupplement_ == null) {
                    _enclosedIdeographicSupplement_ = CodeSetFull.From (0x1F200, 0x1F2FF);
                }
                return _enclosedIdeographicSupplement_;
            }
        }
        private static ICodeSet _enclosedIdeographicSupplement_;

        /// <summary>
        /// 1F300..1F5FF; Miscellaneous Symbols and Pictographs
        /// </summary>
        public static ICodeSet MiscellaneousSymbolsandPictographs {
            get {
                if (_miscellaneousSymbolsandPictographs_ == null) {
                    _miscellaneousSymbolsandPictographs_ = CodeSetFull.From (0x1F300, 0x1F5FF);
                }
                return _miscellaneousSymbolsandPictographs_;
            }
        }
        private static ICodeSet _miscellaneousSymbolsandPictographs_;

        /// <summary>
        /// 1F600..1F64F; Emoticons
        /// </summary>
        public static ICodeSet Emoticons {
            get {
                if (_emoticons_ == null) {
                    _emoticons_ = CodeSetFull.From (0x1F600, 0x1F64F);
                }
                return _emoticons_;
            }
        }
        private static ICodeSet _emoticons_;

        /// <summary>
        /// 1F650..1F67F; Ornamental Dingbats
        /// </summary>
        public static ICodeSet OrnamentalDingbats {
            get {
                if (_ornamentalDingbats_ == null) {
                    _ornamentalDingbats_ = CodeSetFull.From (0x1F650, 0x1F67F);
                }
                return _ornamentalDingbats_;
            }
        }
        private static ICodeSet _ornamentalDingbats_;

        /// <summary>
        /// 1F680..1F6FF; Transport and Map Symbols
        /// </summary>
        public static ICodeSet TransportandMapSymbols {
            get {
                if (_transportandMapSymbols_ == null) {
                    _transportandMapSymbols_ = CodeSetFull.From (0x1F680, 0x1F6FF);
                }
                return _transportandMapSymbols_;
            }
        }
        private static ICodeSet _transportandMapSymbols_;

        /// <summary>
        /// 1F700..1F77F; Alchemical Symbols
        /// </summary>
        public static ICodeSet AlchemicalSymbols {
            get {
                if (_alchemicalSymbols_ == null) {
                    _alchemicalSymbols_ = CodeSetFull.From (0x1F700, 0x1F77F);
                }
                return _alchemicalSymbols_;
            }
        }
        private static ICodeSet _alchemicalSymbols_;

        /// <summary>
        /// 1F780..1F7FF; Geometric Shapes Extended
        /// </summary>
        public static ICodeSet GeometricShapesExtended {
            get {
                if (_geometricShapesExtended_ == null) {
                    _geometricShapesExtended_ = CodeSetFull.From (0x1F780, 0x1F7FF);
                }
                return _geometricShapesExtended_;
            }
        }
        private static ICodeSet _geometricShapesExtended_;

        /// <summary>
        /// 1F800..1F8FF; Supplemental Arrows-C
        /// </summary>
        public static ICodeSet SupplementalArrowsC {
            get {
                if (_supplementalArrowsC_ == null) {
                    _supplementalArrowsC_ = CodeSetFull.From (0x1F800, 0x1F8FF);
                }
                return _supplementalArrowsC_;
            }
        }
        private static ICodeSet _supplementalArrowsC_;

        /// <summary>
        /// 1F900..1F9FF; Supplemental Symbols and Pictographs
        /// </summary>
        public static ICodeSet SupplementalSymbolsandPictographs {
            get {
                if (_supplementalSymbolsandPictographs_ == null) {
                    _supplementalSymbolsandPictographs_ = CodeSetFull.From (0x1F900, 0x1F9FF);
                }
                return _supplementalSymbolsandPictographs_;
            }
        }
        private static ICodeSet _supplementalSymbolsandPictographs_;

        /// <summary>
        /// 20000..2A6DF; CJK Unified Ideographs Extension B
        /// </summary>
        public static ICodeSet CJKUnifiedIdeographsExtensionB {
            get {
                if (_cJKUnifiedIdeographsExtensionB_ == null) {
                    _cJKUnifiedIdeographsExtensionB_ = CodeSetFull.From (0x20000, 0x2A6DF);
                }
                return _cJKUnifiedIdeographsExtensionB_;
            }
        }
        private static ICodeSet _cJKUnifiedIdeographsExtensionB_;

        /// <summary>
        /// 2A700..2B73F; CJK Unified Ideographs Extension C
        /// </summary>
        public static ICodeSet CJKUnifiedIdeographsExtensionC {
            get {
                if (_cJKUnifiedIdeographsExtensionC_ == null) {
                    _cJKUnifiedIdeographsExtensionC_ = CodeSetFull.From (0x2A700, 0x2B73F);
                }
                return _cJKUnifiedIdeographsExtensionC_;
            }
        }
        private static ICodeSet _cJKUnifiedIdeographsExtensionC_;

        /// <summary>
        /// 2B740..2B81F; CJK Unified Ideographs Extension D
        /// </summary>
        public static ICodeSet CJKUnifiedIdeographsExtensionD {
            get {
                if (_cJKUnifiedIdeographsExtensionD_ == null) {
                    _cJKUnifiedIdeographsExtensionD_ = CodeSetFull.From (0x2B740, 0x2B81F);
                }
                return _cJKUnifiedIdeographsExtensionD_;
            }
        }
        private static ICodeSet _cJKUnifiedIdeographsExtensionD_;

        /// <summary>
        /// 2B820..2CEAF; CJK Unified Ideographs Extension E
        /// </summary>
        public static ICodeSet CJKUnifiedIdeographsExtensionE {
            get {
                if (_cJKUnifiedIdeographsExtensionE_ == null) {
                    _cJKUnifiedIdeographsExtensionE_ = CodeSetFull.From (0x2B820, 0x2CEAF);
                }
                return _cJKUnifiedIdeographsExtensionE_;
            }
        }
        private static ICodeSet _cJKUnifiedIdeographsExtensionE_;

        /// <summary>
        /// 2F800..2FA1F; CJK Compatibility Ideographs Supplement
        /// </summary>
        public static ICodeSet CJKCompatibilityIdeographsSupplement {
            get {
                if (_cJKCompatibilityIdeographsSupplement_ == null) {
                    _cJKCompatibilityIdeographsSupplement_ = CodeSetFull.From (0x2F800, 0x2FA1F);
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
                    _tags_ = CodeSetFull.From (0xE0000, 0xE007F);
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
                    _variationSelectorsSupplement_ = CodeSetFull.From (0xE0100, 0xE01EF);
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
                    _supplementaryPrivateUseAreaA_ = CodeSetFull.From (0xF0000, 0xFFFFF);
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
                    _supplementaryPrivateUseAreaB_ = CodeSetFull.From (0x100000, 0x10FFFF);
                }
                return _supplementaryPrivateUseAreaB_;
            }
        }
        private static ICodeSet _supplementaryPrivateUseAreaB_;

    }
}
