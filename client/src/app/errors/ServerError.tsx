import { Divider, Paper, Typography } from "@mui/material";
import { useLocation } from "react-router-dom";

const ServerError = () => {
  const { state } = useLocation();
  return (
    <div>
      <Paper>
        {state?.error ? (
          <>
            <Typography
              variant="h3"
              gutterBottom
              color="secondary"
              sx={{ px: 4, pt: 2 }}
            >
              {state.error.title}
            </Typography>
            <Divider />
            <Typography variant="body1" sx={{ p: 4 }}>
              {state.error.detail}
            </Typography>
          </>
        ) : (
          <>
            <Typography variant="h4" gutterBottom>
              Server Error
            </Typography>
          </>
        )}
      </Paper>
    </div>
  );
};

export default ServerError;
