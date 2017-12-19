package team.incorpore.dr0gis.elqueue.main;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import java.util.List;

import team.incorpore.dr0gis.elqueue.R;
import team.incorpore.dr0gis.elqueue.server.queue.QueueResult;

/**
 * Created by dr0gi on 06.12.2017.
 */

public class QueueAdapter extends RecyclerView.Adapter<QueueHolder> {

    private List<QueueResult> queueList;
    private MainActivity.OpenUsersList listener;

    QueueAdapter(List<QueueResult> queueList, MainActivity.OpenUsersList listener) {
        this.queueList = queueList;
        this.listener = listener;
    }

    @Override
    public QueueHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        LayoutInflater li = LayoutInflater.from(parent.getContext());
        View view = li.inflate(R.layout.item_queue, parent, false);

        return new QueueHolder(view);
    }

    @Override
    public void onBindViewHolder(QueueHolder holder, int position) {
        listener.setPosition(position);
        holder.bindData(queueList.get(position), listener);
    }

    @Override
    public int getItemCount() {
        return queueList.size();
    }
}
