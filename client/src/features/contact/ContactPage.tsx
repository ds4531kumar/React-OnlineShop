import { decrement, increment } from "./counterReducer";
import { Button, ButtonGroup, Typography } from "@mui/material";
import { useAppDispatch, useAppSelector } from "../../app/store/store";

const ContactPage = () => {
  const { data } = useAppSelector((state) => state.counter);
  const dispatch = useAppDispatch();
  return (
    <>
      <Typography variant="h3" gutterBottom>
        Contact Page
      </Typography>
      <Typography variant="h5" gutterBottom>
        The data from the Redux store is: {data}
      </Typography>
      <ButtonGroup>
        <Button onClick={() => dispatch(increment(1))} color="error">
          Increment
        </Button>
        <Button onClick={() => dispatch(decrement(1))} color="secondary">
          Decrement
        </Button>
        <Button onClick={() => dispatch(increment(5))} color="primary">
          Increment by 5
        </Button>
      </ButtonGroup>
    </>
  );
};

export default ContactPage;
