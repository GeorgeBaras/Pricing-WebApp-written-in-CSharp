using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial.MyClasses
{
    public class CAPValuationCalculator : ValuationCalculator
    {
        public const decimal ADJUSTMENT_PERCENTAGE = 0.003m;

        public decimal calculate(PriceRecord priceRecord, int currentMileage)
        {
            // Exact
            PriceBand currentPriceBand = findExactPriceBand(priceRecord, currentMileage);
            if (currentPriceBand != null) {
                return currentPriceBand.getValuation();
            }
            // Between
            if (isBetweenPriceBands(priceRecord, currentMileage)) {
                return calculatePriceBetweenTwoBands(findClosestBandBelowMileage(priceRecord, currentMileage), 
                    findClosestBandAboveMileage(priceRecord, currentMileage), currentMileage);
            }
            // Above Or Below
            currentPriceBand = findClosestBandAboveMileage(priceRecord, currentMileage);
            if (currentPriceBand != null)
            {
                return calculatePriceFromBand(currentPriceBand, currentMileage);
            }
            else {
                currentPriceBand = findClosestBandBelowMileage(priceRecord, currentMileage);
                return calculatePriceFromBand(currentPriceBand, currentMileage);
            }
        }

        private decimal calculatePriceFromBand(PriceBand closestPriceBand, int currentMileage) {
            if (currentMileage > closestPriceBand.getMileage())
            {
                return adjustPriceUp(closestPriceBand.getValuation(), currentMileage - closestPriceBand.getMileage());
            }
            else {
                return adjustPriceDown(closestPriceBand.getValuation(), closestPriceBand.getMileage() - currentMileage);
            }
        }

        private decimal adjustPriceUp(decimal valuation, int mileageAdjustment ) {
            for (int i = 0; i < mileageAdjustment; i++)
            {
                valuation = valuation + (Decimal.Multiply(valuation, ADJUSTMENT_PERCENTAGE));
            }
            return valuation;
        }

        private decimal adjustPriceDown(decimal valuation, int mileageAdjustment)
        {
            for (int i=0; i<mileageAdjustment; i++)
            {
                valuation = valuation - (Decimal.Multiply(valuation,ADJUSTMENT_PERCENTAGE));
            }
            return valuation;
        }

        private decimal calculatePriceBetweenTwoBands(PriceBand bandBelow, PriceBand bandAbove, int currentMileage)
        {
            int mileageDifferenceBetweenBands = bandAbove.getMileage() - bandBelow.getMileage();
            decimal priceDifferenceBetweenBands = bandBelow.getValuation() - bandAbove.getValuation();
            decimal priceAdjustment = priceDifferenceBetweenBands / mileageDifferenceBetweenBands;
            decimal finalValuation = ((currentMileage - bandBelow.getMileage()) * priceAdjustment) + bandBelow.getValuation();
            return finalValuation;
        }

        private PriceBand findClosestBandBelowMileage(PriceRecord priceRecord, int currentMileage) {
            List<PriceBand> priceBandsInReverse = priceRecord.getPriceBands();
            priceBandsInReverse.Reverse();
            foreach (PriceBand priceBand in priceBandsInReverse)
            {
                if (currentMileage > priceBand.getMileage())
                {
                    return priceBand;
                }
            }
            return null;
        }

        private PriceBand findClosestBandAboveMileage(PriceRecord priceRecord, int currentMileage)
        {
            foreach (PriceBand priceBand in priceRecord.getPriceBands())
            {
                if (currentMileage < priceBand.getMileage())
                {
                    return priceBand;
                }
            }
            return null;
        }

        private bool isBetweenPriceBands(PriceRecord priceRecord, int currentMileage) {
            if (currentMileage > priceRecord.getPriceBands().First().getValuation() && currentMileage < priceRecord.getPriceBands().Last().getValuation()) {
                return true;
            }
            return false;
        }

        private PriceBand findExactPriceBand(PriceRecord priceRecord, int currentMileage) {
            foreach (PriceBand priceBand in priceRecord.getPriceBands())
            {
                if (priceBand.getMileage().Equals(currentMileage)){
                    return priceBand;
                }
            }
            return null;
        }
    }
}