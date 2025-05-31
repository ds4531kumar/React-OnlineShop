import { Grid } from "@mui/material";
import OrderSummary from "../../app/shared/components/OrderSummary";

export default function CheckoutPage() {
  //const { data: basket } = useFetchBasketQuery();
  //const [createPaymentIntent, { isLoading }] = useCreatePaymentIntentMutation();
  //const created = useRef(false);
  //const { darkMode } = useAppSelector((state) => state.ui);

  // useEffect(() => {
  //   if (!created.current) createPaymentIntent();
  //   created.current = true;
  // }, [createPaymentIntent]);

  // const options: StripeElementsOptions | undefined = useMemo(() => {
  //   if (!basket?.clientSecret) return undefined;
  //   return {
  //     clientSecret: basket.clientSecret,
  //     appearance: {
  //       labels: "floating",
  //       theme: darkMode ? "night" : "stripe",
  //     },
  //   };
  // }, [basket?.clientSecret, darkMode]);

  return (
    <Grid container spacing={2}>
      <Grid size={8}>
       Only authorized users can access this page.
      </Grid>
      {/* <Grid size={4}>
        <OrderSummary />
      </Grid> */}
    </Grid>
  );
}
