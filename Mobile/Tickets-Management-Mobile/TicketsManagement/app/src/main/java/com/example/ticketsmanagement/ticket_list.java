package com.example.ticketsmanagement;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import java.util.ArrayList;
import java.util.List;

public class ticket_list extends AppCompatActivity {

    private RecyclerView recyclerViewTickets;
    private TicketsAdapter ticketsAdapter;
    private Button buttonBack;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_ticket_list);

        recyclerViewTickets = findViewById(R.id.recyclerViewTickets);
        buttonBack = findViewById(R.id.buttonBack);

        int userId = getIntent().getIntExtra("userId", -1);

        List<DataArray.Ticket> userRequests = getUserRequests(userId);

        recyclerViewTickets.setLayoutManager(new LinearLayoutManager(this));
        ticketsAdapter = new TicketsAdapter(this, userRequests);
        recyclerViewTickets.setAdapter(ticketsAdapter);

        buttonBack.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });
    }

    private List<DataArray.Ticket> getUserRequests(int userId) {
        List<DataArray.Ticket> userRequests = new ArrayList<>();
        for (DataArray.Ticket request : DataArray.Tickets) {
            if (request.ClientId == userId) {
                userRequests.add(request);
            }
        }
        return userRequests;
    }
}