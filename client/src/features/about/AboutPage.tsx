import {
  Alert,
  AlertTitle,
  Button,
  ButtonGroup,
  Container,
  List,
  ListItem,
  Typography,
} from "@mui/material";
import {
  useLazyGet400ErrorQuery,
  useLazyGet401ErrorQuery,
  useLazyGet404ErrorQuery,
  useLazyGet500ErrorQuery,
  useLazyGetValidationErrorQuery,
} from "./errorApi";
import { useState } from "react";

const AboutPage = () => {
  const [validationErrors, setValidationErrors] = useState<string[]>([]);

  const [trigger400Error] = useLazyGet400ErrorQuery();
  const [trigger401Error] = useLazyGet401ErrorQuery();
  const [trigger404Error] = useLazyGet404ErrorQuery();
  const [trigger500Error] = useLazyGet500ErrorQuery();
  const [triggerValidationError] = useLazyGetValidationErrorQuery();

  const getValidationError = async () => {
    try {
      await triggerValidationError().unwrap();
    } catch (error: unknown) {
      if (
        error &&
        typeof error === "object" &&
        "message" in error &&
        typeof (error as { message: unknown }).message === "string"
      ) {
        const errorArray = (error as { message: string }).message.split(",");
        setValidationErrors(errorArray);
        console.log(errorArray);
      }
    }
  };
  return (
    <Container maxWidth="lg">
      <Typography gutterBottom variant="h3">
        Error for testing
      </Typography>
      <ButtonGroup>
        <Button
          variant="contained"
          onClick={() => trigger400Error().catch((err) => console.log(err))}
          color="primary"
        >
          Test 400 Error
        </Button>

        <Button
          variant="contained"
          onClick={() => trigger404Error().catch((err) => console.log(err))}
          color="primary"
        >
          Test 404 Error
        </Button>

        <Button
          variant="contained"
          onClick={() => trigger401Error().catch((err) => console.log(err))}
          color="primary"
        >
          Test 401 Error
        </Button>

        <Button
          variant="contained"
          onClick={() => trigger500Error().catch((err) => console.log(err))}
          color="primary"
        >
          Test 500 Error
        </Button>

        <Button
          variant="contained"
          onClick={getValidationError}
          color="primary"
        >
          Test Validation Error
        </Button>
      </ButtonGroup>
      {validationErrors.length > 0 && (
        <Alert severity="error" sx={{ mt: 2 }}>
          <AlertTitle>Validation Errors</AlertTitle>
          <List>
            {validationErrors.map((error, index) => (
              <ListItem key={index}>{error}</ListItem>
            ))}
          </List>
        </Alert>
      )}
    </Container>
  );
};

export default AboutPage;
