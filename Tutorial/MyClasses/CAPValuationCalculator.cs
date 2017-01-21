using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial.MyClasses
{
    public class CAPValuationCalculator : ValuationCalculator
    {
        public static readonly decimal ADJUSTMENT_PERCENTAGE = 0.003m;

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

        protected decimal calculatePriceFromBand(PriceBand closestPriceBand, int currentMileage) {
            if (currentMileage < closestPriceBand.getMileage())
            {
                return adjustPriceUp(closestPriceBand.getValuation(), Math.Abs(currentMileage - closestPriceBand.getMileage()));
            }
            else {
                return adjustPriceDown(closestPriceBand.getValuation(), Math.Abs(currentMileage - closestPriceBand.getMileage()));
            }
        }

        protected decimal adjustPriceUp(decimal valuation, int mileageAdjustment ) {
            for (int i = 0; i < mileageAdjustment; i++)
            {
                valuation = valuation + (Decimal.Multiply(valuation, ADJUSTMENT_PERCENTAGE));
            }
            return valuation;
        }

        protected decimal adjustPriceDown(decimal valuation, int mileageAdjustment)
        {
            for (int i=0; i<mileageAdjustment; i++)
            {
                valuation = valuation - (Decimal.Multiply(valuation,ADJUSTMENT_PERCENTAGE));
            }
            return valuation;
        }

        protected decimal calculatePriceBetweenTwoBands(PriceBand bandBelow, PriceBand bandAbove, int currentMileage)
        {
            int mileageDifferenceBetweenBands = bandAbove.getMileage() - bandBelow.getMileage();
            decimal priceDifferenceBetweenBands = bandBelow.getValuation() - bandAbove.getValuation();
            decimal priceAdjustment = priceDifferenceBetweenBands / mileageDifferenceBetweenBands;
            decimal finalValuation = bandBelow.getValuation() - ((currentMileage - bandBelow.getMileage()) * priceAdjustment);
            return finalValuation;
        }

        protected PriceBand findClosestBandBelowMileage(PriceRecord priceRecord, int currentMileage) {
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

        protected PriceBand findClosestBandAboveMileage(PriceRecord priceRecord, int currentMileage)
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

        protected bool isBetweenPriceBands(PriceRecord priceRecord, int currentMileage) {
            if (currentMileage > priceRecord.getPriceBands().First().getMileage() && currentMileage < priceRecord.getPriceBands().Last().getMileage()) {
                return true;
            }
            return false;
        }

        protected PriceBand findExactPriceBand(PriceRecord priceRecord, int currentMileage) {
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