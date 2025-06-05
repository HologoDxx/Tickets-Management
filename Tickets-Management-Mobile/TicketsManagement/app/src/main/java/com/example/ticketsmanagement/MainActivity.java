package com.example.ticketsmanagement;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {

    private EditText editTextLogin;
    private EditText editTextPassword;
    private Button buttonLogin;
    private TextView textViewError;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        editTextLogin = findViewById(R.id.editTextLogin);
        editTextPassword = findViewById(R.id.editTextPassword);
        buttonLogin = findViewById(R.id.buttonLogin);
        textViewError = findViewById(R.id.textViewError);

        buttonLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String login = editTextLogin.getText().toString();
                String password = editTextPassword.getText().toString();

                int userId = authenticateUser(login, password);

                if (userId != -1) {
                    textViewError.setVisibility(View.GONE);
                    Intent intent = new Intent(MainActivity.this, ticket_list.class);
                    intent.putExtra("userId", userId);
                    startActivity(intent);
                } else {
                    textViewError.setVisibility(View.VISIBLE);
                }
            }
        });
    }

    private int authenticateUser(String login, String password) {
        for (DataArray.User user : DataArray.Users) {
            if (user.Login.equals(login) && user.Password.equals(password)) {
                return user.Id;
            }
        }
        return -1;
    }
}