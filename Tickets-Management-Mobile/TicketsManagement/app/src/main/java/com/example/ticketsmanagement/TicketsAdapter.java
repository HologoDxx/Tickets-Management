package com.example.ticketsmanagement;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import java.util.List;

public class TicketsAdapter extends RecyclerView.Adapter<TicketsAdapter.TicketsViewHolder> {

    private List<DataArray.Ticket> tickets;
    private Context context;

    public TicketsAdapter(Context context, List<DataArray.Ticket> tickets) {
        this.context = context;
        this.tickets = tickets;
    }

    @NonNull
    @Override
    public TicketsViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(context).inflate(R.layout.ticket_item, parent, false);
        return new TicketsViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull TicketsViewHolder holder, int position) {
        DataArray.Ticket ticket = tickets.get(position);

        holder.textViewTicketId.setText("Номер заявки: " + ticket.Id);
        holder.textViewCreationDate.setText(ticket.CreationDate);
        holder.textViewEquipment.setText(ticket.Equipment);
        holder.textViewFaultType.setText(ticket.FaultType);
        holder.textViewDescription.setText(ticket.Description);
        holder.textViewStatus.setText(ticket.Status);
        holder.textViewDateCompleted.setText(ticket.DateCompleted != null && !ticket.DateCompleted.isEmpty() ? ticket.DateCompleted : "-");
    }

    @Override
    public int getItemCount() {
        return tickets.size();
    }

    public static class TicketsViewHolder extends RecyclerView.ViewHolder {
        TextView textViewTicketId;
        TextView textViewCreationDate;
        TextView textViewEquipment;
        TextView textViewFaultType;
        TextView textViewDescription;
        TextView textViewStatus;
        TextView textViewDateCompleted;

        public TicketsViewHolder(@NonNull View itemView) {
            super(itemView);
            textViewTicketId = itemView.findViewById(R.id.textViewTicketId);
            textViewCreationDate = itemView.findViewById(R.id.textViewCreationDate);
            textViewEquipment = itemView.findViewById(R.id.textViewEquipment);
            textViewFaultType = itemView.findViewById(R.id.textViewFaultType);
            textViewDescription = itemView.findViewById(R.id.textViewDescription);
            textViewStatus = itemView.findViewById(R.id.textViewStatus);
            textViewDateCompleted = itemView.findViewById(R.id.textViewDateCompleted);
        }
    }
}
