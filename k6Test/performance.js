import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  stages: [
    { duration: "1m", target: 20 },
    { duration: "1m", target: 20 },
    { duration: "1m", target: 0 },
  ],
  thresholds: {
    http_req_duration: ["p(95) < 300"],
    http_req_failed: ["rate==0"],
  },
  discardResponseBodies: false,
};

// ⭐ BASE URL of your deployed site (homepage contains the BP form)
const BASE_URL = "https://bp-ca1-cwhjbgg7b2f4h8ch.italynorth-01.azurewebsites.net";

export default function () {

  // 1) GET home page
  const resGet = http.get(BASE_URL);

  check(resGet, {
    "GET returned 200": (r) => r.status === 200,
  });

  // 2) Extract anti-forgery token
  const tokenMatch = resGet.body.match(/name="__RequestVerificationToken".+?value="([^"]+)"/);
  const token = tokenMatch && tokenMatch[1];

  check(token, {
    "anti-forgery token found": () => token !== undefined,
  });

  // 3) Build POST payload using correct BP field names
  const payload = {
    "__RequestVerificationToken": token,
    "BP.Systolic": 120,
    "BP.Diastolic": 80,
  };

  // 4) POST to homepage again (same URL)
  const resPost = http.post(
    BASE_URL,
    payload,
    {
      headers: {
        "Content-Type": "application/x-www-form-urlencoded",
      },
    }
  );

  check(resPost, {
    "POST returned OK or redirect": (r) => r.status === 200 || r.status === 302,
  });

  sleep(3);
}
