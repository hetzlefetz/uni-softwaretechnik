using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Verlag
{
    internal class CalcHonorar
    {
        private const decimal DEFAULT_CHAR_PRICE = 0.1M;
        private const decimal DEFAULT_PIC_PRICE = 1M;

        private readonly decimal _picPrice;
        private readonly decimal _charPrice;


        private readonly string _pictureIdentifier = "Picture";

        public CalcHonorar()
        {
            _picPrice = DEFAULT_PIC_PRICE;
            _charPrice = DEFAULT_CHAR_PRICE;
        }

        public CalcHonorar(IPricingService pc)
        {
            _picPrice = pc?.GetPicturePrice() ?? DEFAULT_PIC_PRICE;
            _charPrice = pc?.GetCharPrice() ?? DEFAULT_CHAR_PRICE;
        }
        public decimal calc(string book)
        {
            if (string.IsNullOrWhiteSpace(book))
            {
                return 0;
            }
            return ((decimal)countPics(book) * _picPrice) + ((decimal)countText(book) * _charPrice);

        }

        internal int countPics(string book)
        {
            if (string.IsNullOrWhiteSpace(book))
            {
                return 0;
            }
            return Regex.Matches(book, _pictureIdentifier).Count;
        }

        internal int countText(string book)
        {
            if (string.IsNullOrWhiteSpace(book))
            {
                return 0;
            }
            return book?.Replace(_pictureIdentifier, "")?.Count() ?? 0;
        }
    }
}
