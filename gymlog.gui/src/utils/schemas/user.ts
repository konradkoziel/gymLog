import { z } from "zod";

export const RegisterSchema = z
  .object({
    email: z
      .string()
      .email({ message: "Invalid email format!" })
      .min(8, { message: "Email must be at least 8 characters long!" })
      .max(100, { message: "Email must be at most 100 characters long!" }),
    password: z
      .string()
      .min(8, { message: "Password must be at least 8 characters long!" })
      .max(50, { message: "Password must be at most 50 characters long!" }),

    confirmPassword: z
      .string()
      .min(8, { message: "Password must be at least 8 characters long!" })
      .max(50, { message: "Password must be at most 50 characters long!" })
      .regex(/[A-Z]/, {
        message: "Password must contain at least one uppercase letter!",
      })
      .regex(/[0-9!@#$%^&*(),.?":{}|<>]/, {
        message:
          "Password must contain at least one number or special character!",
      }),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: "Passwords do not match!",
    path: ["confirmPassword"],
  });

export const LoginSchema = z.object({
  email: z.string().email({ message: "Email is required!" }).min(8).max(50),
  password: z.string().min(8, { message: "Password is required!" }).max(50),
  code: z.optional(z.string()),
});

export const ResetPasswordSchema = z.object({
  email: z.string().email({ message: "Email is required!" }).min(8).max(50),
});

export const NewPasswordSchema = z
  .object({
    password: z
      .string()
      .min(8, { message: "Password must be at least 8 characters long!" })
      .max(50, { message: "Password must be at most 50 characters long!" }),
    confirmPassword: z
      .string()
      .min(8, { message: "Password must be at least 8 characters long!" })
      .max(50, { message: "Password must be at most 50 characters long!" }),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: "Passwords do not match!",
    path: ["confirmPassword"],
  });


